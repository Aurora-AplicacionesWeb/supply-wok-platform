using Aurora.SupplyWok.Platform.Iam.Application.Internal.OutboundServices;
using Aurora.SupplyWok.Platform.Iam.Interfaces.Acl;
using Aurora.SupplyWok.Platform.Profiles.Interfaces.Acl;
using Aurora.SupplyWok.Platform.Shared.Application.Model;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;
using Aurora.SupplyWok.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Aurora.SupplyWok.Platform.Subscriptions.Application.CommandServices;
using Aurora.SupplyWok.Platform.Subscriptions.Application.Internal.OutboundServices;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.ValueObjects;
using Aurora.SupplyWok.Platform.Subscriptions.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Stripe.Configuration;

namespace Aurora.SupplyWok.Platform.Subscriptions.Application.Internal.CommandServices;

public class SubscriptionRegistrationCommandService(
    IPendingRegistrationRepository pendingRegistrationRepository,
    ISubscriptionRepository subscriptionRepository,
    IProcessedWebhookEventRepository processedWebhookEventRepository,
    IIamContextFacade iamContextFacade,
    IProfilesContextFacade profilesContextFacade,
    IHashingService hashingService,
    IStripeCheckoutService stripeCheckoutService,
    IUnitOfWork unitOfWork,
    AppDbContext appDbContext,
    IOptions<StripeSettings> stripeOptions) : ISubscriptionRegistrationCommandService
{
    private readonly StripeSettings _stripeSettings = stripeOptions.Value;

    public async Task<Result<(PendingRegistration registration, string checkoutUrl)>> Handle(
        StartSubscriptionRegistrationCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var validation = Validate(command);
            if (validation is not null)
                return Result<(PendingRegistration registration, string checkoutUrl)>.Failure(validation.Value.Error,
                    validation.Value.Message);

            var existingUserId = await iamContextFacade.FetchUserIdByEmail(command.Email.Trim(), cancellationToken);
            if (existingUserId > 0)
                return Result<(PendingRegistration registration, string checkoutUrl)>.Failure(
                    SubscriptionsError.DuplicateEmail,
                    "A provisioned user already exists with the same email.");

            var role = command.Role.Trim().ToLowerInvariant();
            var planCode = ParsePlanCode(command.PlanCode);
            var passwordHash = hashingService.HashPassword(command.Password);
            var pendingRegistration = new PendingRegistration(
                command.Email,
                passwordHash,
                role,
                planCode,
                command.BusinessName,
                command.FirstName,
                command.LastName,
                command.Street,
                command.District,
                command.City,
                command.Country,
                command.ContactEmail,
                command.Phone,
                command.Category,
                DateTimeOffset.UtcNow.AddDays(1));

            await pendingRegistrationRepository.AddAsync(pendingRegistration, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            var checkoutResult = await stripeCheckoutService.CreateSubscriptionCheckoutSessionAsync(
                pendingRegistration.PublicId,
                pendingRegistration.Email,
                pendingRegistration.Role,
                pendingRegistration.PlanCode.ToString().ToLowerInvariant(),
                cancellationToken);

            if (!checkoutResult.IsSuccess || string.IsNullOrWhiteSpace(checkoutResult.SessionId) ||
                string.IsNullOrWhiteSpace(checkoutResult.CheckoutUrl))
            {
                pendingRegistration.MarkFailed();
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<(PendingRegistration registration, string checkoutUrl)>.Failure(
                    SubscriptionsError.StripeSessionCreationFailed,
                    checkoutResult.ErrorMessage ?? "Stripe session could not be created.");
            }

            pendingRegistration.AssignCheckoutSession(checkoutResult.SessionId);
            await unitOfWork.CompleteAsync(cancellationToken);

            return Result<(PendingRegistration registration, string checkoutUrl)>.Success((pendingRegistration,
                checkoutResult.CheckoutUrl));
        }
        catch (OperationCanceledException)
        {
            return Result<(PendingRegistration registration, string checkoutUrl)>.Failure(
                SubscriptionsError.OperationCancelled, nameof(SubscriptionsError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result<(PendingRegistration registration, string checkoutUrl)>.Failure(
                SubscriptionsError.DatabaseError, nameof(SubscriptionsError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result<(PendingRegistration registration, string checkoutUrl)>.Failure(
                SubscriptionsError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result> Handle(ProvisionSubscriptionRegistrationCommand command, CancellationToken cancellationToken)
    {
        PendingRegistration? pendingRegistration = null;
        await using var transaction = await appDbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            if (await processedWebhookEventRepository.ExistsByStripeEventIdAsync(command.StripeEventId, cancellationToken))
                return Result.Success();

            pendingRegistration =
                await pendingRegistrationRepository.FindByPublicIdAsync(command.PendingRegistrationPublicId,
                    cancellationToken);
            if (pendingRegistration is null)
                return Result.Failure(SubscriptionsError.RegistrationNotFound,
                    nameof(SubscriptionsError.RegistrationNotFound));

            if (pendingRegistration.Status == EPendingRegistrationStatus.Provisioned)
            {
                await processedWebhookEventRepository.AddAsync(
                    new ProcessedWebhookEvent(command.StripeEventId, "invoice.paid"), cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return Result.Success();
            }

            if (pendingRegistration.ExpiresAt <= DateTimeOffset.UtcNow)
                return Result.Failure(SubscriptionsError.RegistrationExpired,
                    nameof(SubscriptionsError.RegistrationExpired));

            pendingRegistration.UpdateStripeReferences(command.StripeCustomerId, command.StripeSubscriptionId);

            var existingUserId = await iamContextFacade.FetchUserIdByEmail(pendingRegistration.Email, cancellationToken);
            if (existingUserId > 0)
            {
                var existingSubscription = await subscriptionRepository.FindByStripeSubscriptionIdAsync(
                    command.StripeSubscriptionId, cancellationToken);
                var userSubscription = await subscriptionRepository.FindByUserIdAsync(existingUserId, cancellationToken);
                if (existingSubscription is not null || userSubscription is not null)
                {
                    pendingRegistration.MarkProvisioned(command.StripeCustomerId, command.StripeSubscriptionId);
                    await processedWebhookEventRepository.AddAsync(
                        new ProcessedWebhookEvent(command.StripeEventId, "invoice.paid"), cancellationToken);
                    await unitOfWork.CompleteAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                    return Result.Success();
                }

                var recoveredProfileId = await EnsureProfileExistsAsync(pendingRegistration, existingUserId, cancellationToken);
                if (recoveredProfileId <= 0)
                    return Result.Failure(SubscriptionsError.ProvisioningFailed,
                        "Existing user could not be linked to a profile for subscription provisioning.");

                var recoveredSubscription = new Subscription(
                    existingUserId,
                    pendingRegistration.Role,
                    pendingRegistration.PlanCode,
                    ParseSubscriptionStatus(command.StripeSubscriptionStatus),
                    command.StripeCustomerId,
                    command.StripeSubscriptionId,
                    command.StripePriceId,
                    command.CurrentPeriodStart,
                    command.CurrentPeriodEnd);

                await subscriptionRepository.AddAsync(recoveredSubscription, cancellationToken);
                pendingRegistration.MarkProvisioned(command.StripeCustomerId, command.StripeSubscriptionId);
                await processedWebhookEventRepository.AddAsync(
                    new ProcessedWebhookEvent(command.StripeEventId, "invoice.paid"), cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return Result.Success();
            }

            var userId = await iamContextFacade.CreateUser(
                pendingRegistration.Email,
                pendingRegistration.PasswordHash,
                pendingRegistration.Role,
                cancellationToken);
            if (userId <= 0)
                throw new InvalidOperationException("User provisioning failed.");

            var profileId = pendingRegistration.Role == "supplier"
                ? await profilesContextFacade.CreateSupplierProfile(
                    pendingRegistration.BusinessName,
                    pendingRegistration.FirstName,
                    pendingRegistration.LastName,
                    pendingRegistration.Street,
                    pendingRegistration.District,
                    pendingRegistration.City,
                    pendingRegistration.Country,
                    pendingRegistration.ContactEmail,
                    pendingRegistration.Phone ?? string.Empty,
                    pendingRegistration.Category ?? string.Empty,
                    userId,
                    cancellationToken)
                : await profilesContextFacade.CreateRestaurantProfile(
                    pendingRegistration.BusinessName,
                    pendingRegistration.FirstName,
                    pendingRegistration.LastName,
                    pendingRegistration.Street,
                    pendingRegistration.District,
                    pendingRegistration.City,
                    pendingRegistration.Country,
                    pendingRegistration.ContactEmail,
                    userId,
                    cancellationToken);

            if (profileId <= 0)
                throw new InvalidOperationException("Profile provisioning failed.");

            var subscription = new Subscription(
                userId,
                pendingRegistration.Role,
                pendingRegistration.PlanCode,
                ParseSubscriptionStatus(command.StripeSubscriptionStatus),
                command.StripeCustomerId,
                command.StripeSubscriptionId,
                command.StripePriceId,
                command.CurrentPeriodStart,
                command.CurrentPeriodEnd);

            await subscriptionRepository.AddAsync(subscription, cancellationToken);
            pendingRegistration.MarkProvisioned(command.StripeCustomerId, command.StripeSubscriptionId);
            await processedWebhookEventRepository.AddAsync(
                new ProcessedWebhookEvent(command.StripeEventId, "invoice.paid"), cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            await transaction.RollbackAsync(cancellationToken);
            if (pendingRegistration is not null) await MarkRegistrationFailedAsync(pendingRegistration.PublicId, cancellationToken);
            return Result.Failure(SubscriptionsError.OperationCancelled, nameof(SubscriptionsError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            await transaction.RollbackAsync(cancellationToken);
            if (pendingRegistration is not null) await MarkRegistrationFailedAsync(pendingRegistration.PublicId, cancellationToken);
            return Result.Failure(SubscriptionsError.DatabaseError, nameof(SubscriptionsError.DatabaseError));
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            if (pendingRegistration is not null) await MarkRegistrationFailedAsync(pendingRegistration.PublicId, cancellationToken);
            return Result.Failure(SubscriptionsError.ProvisioningFailed, ex.Message);
        }
    }

    public async Task<Result> Handle(MarkPendingRegistrationExpiredCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var pendingRegistration =
                await pendingRegistrationRepository.FindByPublicIdAsync(command.PendingRegistrationPublicId,
                    cancellationToken);
            if (pendingRegistration is null)
                return Result.Failure(SubscriptionsError.RegistrationNotFound,
                    nameof(SubscriptionsError.RegistrationNotFound));

            pendingRegistration.MarkExpired();
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(SubscriptionsError.OperationCancelled, nameof(SubscriptionsError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result.Failure(SubscriptionsError.DatabaseError, nameof(SubscriptionsError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result.Failure(SubscriptionsError.InternalServerError, ex.Message);
        }
    }

    public async Task<Result> Handle(SyncSubscriptionStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            if (await processedWebhookEventRepository.ExistsByStripeEventIdAsync(command.StripeEventId, cancellationToken))
                return Result.Success();

            var subscription =
                await subscriptionRepository.FindByStripeSubscriptionIdAsync(command.StripeSubscriptionId,
                    cancellationToken);
            if (subscription is null)
                return Result.Failure(SubscriptionsError.SubscriptionNotFound,
                    nameof(SubscriptionsError.SubscriptionNotFound));

            subscription.SyncStatus(
                ParseSubscriptionStatus(command.StripeSubscriptionStatus),
                ParsePlanCodeFromPriceId(command.StripePriceId),
                command.StripePriceId,
                command.CurrentPeriodStart,
                command.CurrentPeriodEnd);

            await processedWebhookEventRepository.AddAsync(
                new ProcessedWebhookEvent(command.StripeEventId, "subscription.sync"), cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result.Success();
        }
        catch (OperationCanceledException)
        {
            return Result.Failure(SubscriptionsError.OperationCancelled, nameof(SubscriptionsError.OperationCancelled));
        }
        catch (DbUpdateException)
        {
            return Result.Failure(SubscriptionsError.DatabaseError, nameof(SubscriptionsError.DatabaseError));
        }
        catch (Exception ex)
        {
            return Result.Failure(SubscriptionsError.InternalServerError, ex.Message);
        }
    }

    private async Task MarkRegistrationFailedAsync(Guid publicId, CancellationToken cancellationToken)
    {
        var failedRegistration = await pendingRegistrationRepository.FindByPublicIdAsync(publicId, cancellationToken);
        if (failedRegistration is null) return;

        if (await TryPromoteToProvisionedAsync(failedRegistration, cancellationToken)) return;

        failedRegistration.MarkFailed();
        await unitOfWork.CompleteAsync(cancellationToken);
    }

    private async Task<int> EnsureProfileExistsAsync(PendingRegistration pendingRegistration, int userId,
        CancellationToken cancellationToken)
    {
        if (pendingRegistration.Role == "supplier")
        {
            var supplierProfileId =
                await profilesContextFacade.FetchSupplierProfileIdByUserId(userId, cancellationToken);
            if (supplierProfileId > 0) return supplierProfileId;

            return await profilesContextFacade.CreateSupplierProfile(
                pendingRegistration.BusinessName,
                pendingRegistration.FirstName,
                pendingRegistration.LastName,
                pendingRegistration.Street,
                pendingRegistration.District,
                pendingRegistration.City,
                pendingRegistration.Country,
                pendingRegistration.ContactEmail,
                pendingRegistration.Phone ?? string.Empty,
                pendingRegistration.Category ?? string.Empty,
                userId,
                cancellationToken);
        }

        var restaurantProfileId =
            await profilesContextFacade.FetchRestaurantProfileIdByUserId(userId, cancellationToken);
        if (restaurantProfileId > 0) return restaurantProfileId;

        return await profilesContextFacade.CreateRestaurantProfile(
            pendingRegistration.BusinessName,
            pendingRegistration.FirstName,
            pendingRegistration.LastName,
            pendingRegistration.Street,
            pendingRegistration.District,
            pendingRegistration.City,
            pendingRegistration.Country,
            pendingRegistration.ContactEmail,
            userId,
            cancellationToken);
    }

    private async Task<bool> TryPromoteToProvisionedAsync(PendingRegistration pendingRegistration,
        CancellationToken cancellationToken)
    {
        if (pendingRegistration.Status == EPendingRegistrationStatus.Provisioned) return true;

        var existingUserId = await iamContextFacade.FetchUserIdByEmail(pendingRegistration.Email, cancellationToken);
        if (existingUserId <= 0) return false;

        var existingProfileId = pendingRegistration.Role == "supplier"
            ? await profilesContextFacade.FetchSupplierProfileIdByUserId(existingUserId, cancellationToken)
            : await profilesContextFacade.FetchRestaurantProfileIdByUserId(existingUserId, cancellationToken);
        if (existingProfileId <= 0) return false;

        Subscription? existingSubscription = null;
        if (!string.IsNullOrWhiteSpace(pendingRegistration.StripeSubscriptionId))
            existingSubscription = await subscriptionRepository.FindByStripeSubscriptionIdAsync(
                pendingRegistration.StripeSubscriptionId, cancellationToken);

        existingSubscription ??= await subscriptionRepository.FindByUserIdAsync(existingUserId, cancellationToken);
        if (existingSubscription is null) return false;

        pendingRegistration.MarkProvisioned(
            pendingRegistration.StripeCustomerId ?? existingSubscription.StripeCustomerId,
            pendingRegistration.StripeSubscriptionId ?? existingSubscription.StripeSubscriptionId);
        await unitOfWork.CompleteAsync(cancellationToken);
        return true;
    }

    private static (SubscriptionsError Error, string Message)? Validate(StartSubscriptionRegistrationCommand command)
    {
        if (string.IsNullOrWhiteSpace(command.Email) || !command.Email.Contains('@'))
            return (SubscriptionsError.InvalidData, "A valid account email is required.");
        if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length < 8)
            return (SubscriptionsError.InvalidData, "Password must be at least 8 characters long.");
        if (string.IsNullOrWhiteSpace(command.Role))
            return (SubscriptionsError.InvalidRole, "Role is required.");
        if (string.IsNullOrWhiteSpace(command.PlanCode))
            return (SubscriptionsError.InvalidPlan, "Plan is required.");
        if (string.IsNullOrWhiteSpace(command.BusinessName) ||
            string.IsNullOrWhiteSpace(command.FirstName) ||
            string.IsNullOrWhiteSpace(command.LastName) ||
            string.IsNullOrWhiteSpace(command.Street) ||
            string.IsNullOrWhiteSpace(command.District) ||
            string.IsNullOrWhiteSpace(command.City) ||
            string.IsNullOrWhiteSpace(command.ContactEmail))
            return (SubscriptionsError.InvalidData, "Complete business profile information is required.");

        var normalizedRole = command.Role.Trim().ToLowerInvariant();
        if (normalizedRole is not ("restaurant" or "supplier"))
            return (SubscriptionsError.InvalidRole, "Role must be restaurant or supplier.");
        if (!TryParsePlanCode(command.PlanCode, out _))
            return (SubscriptionsError.InvalidPlan, "Plan must be Premium or Enterprise.");
        if (normalizedRole == "supplier" && string.IsNullOrWhiteSpace(command.Phone))
            return (SubscriptionsError.InvalidData, "Supplier phone is required.");
        if (normalizedRole == "supplier" && string.IsNullOrWhiteSpace(command.Category))
            return (SubscriptionsError.InvalidData, "Supplier category is required.");

        return null;
    }

    private static bool TryParsePlanCode(string value, out ESubscriptionPlanCode planCode)
    {
        return Enum.TryParse(value?.Trim(), true, out planCode);
    }

    private static ESubscriptionPlanCode ParsePlanCode(string value)
    {
        TryParsePlanCode(value, out var planCode);
        return planCode;
    }

    private static ESubscriptionStatus ParseSubscriptionStatus(string value)
    {
        var normalized = value?.Replace("_", string.Empty).Replace("-", string.Empty).Trim();
        if (Enum.TryParse(normalized, true, out ESubscriptionStatus status)) return status;
        return ESubscriptionStatus.Incomplete;
    }

    private ESubscriptionPlanCode ParsePlanCodeFromPriceId(string stripePriceId)
    {
        return string.Equals(stripePriceId, _stripeSettings.Prices.EnterpriseMonthly, StringComparison.Ordinal)
            ? ESubscriptionPlanCode.Enterprise
            : ESubscriptionPlanCode.Premium;
    }
}
