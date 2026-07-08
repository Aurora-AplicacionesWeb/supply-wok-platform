using Aurora.SupplyWok.Platform.Subscriptions.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Aurora.SupplyWok.Platform.Subscriptions.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySubscriptionsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<PendingRegistration>().ToTable("PendingRegistrations");
        builder.Entity<PendingRegistration>().HasKey(registration => registration.Id);
        builder.Entity<PendingRegistration>().Property(registration => registration.Id).ValueGeneratedOnAdd();
        builder.Entity<PendingRegistration>().Property(registration => registration.PublicId).IsRequired();
        builder.Entity<PendingRegistration>().Property(registration => registration.Email).IsRequired().HasMaxLength(150);
        builder.Entity<PendingRegistration>().Property(registration => registration.PasswordHash).IsRequired().HasMaxLength(255);
        builder.Entity<PendingRegistration>().Property(registration => registration.Role).IsRequired().HasMaxLength(30);
        builder.Entity<PendingRegistration>().Property(registration => registration.PlanCode).HasConversion<string>().IsRequired().HasMaxLength(30);
        builder.Entity<PendingRegistration>().Property(registration => registration.Status).HasConversion<string>().IsRequired().HasMaxLength(30);
        builder.Entity<PendingRegistration>().Property(registration => registration.StripeCheckoutSessionId).HasMaxLength(120);
        builder.Entity<PendingRegistration>().Property(registration => registration.StripeCustomerId).HasMaxLength(120);
        builder.Entity<PendingRegistration>().Property(registration => registration.StripeSubscriptionId).HasMaxLength(120);
        builder.Entity<PendingRegistration>().Property(registration => registration.BusinessName).IsRequired().HasMaxLength(150);
        builder.Entity<PendingRegistration>().Property(registration => registration.FirstName).IsRequired().HasMaxLength(80);
        builder.Entity<PendingRegistration>().Property(registration => registration.LastName).IsRequired().HasMaxLength(80);
        builder.Entity<PendingRegistration>().Property(registration => registration.Street).IsRequired().HasMaxLength(150);
        builder.Entity<PendingRegistration>().Property(registration => registration.District).IsRequired().HasMaxLength(100);
        builder.Entity<PendingRegistration>().Property(registration => registration.City).IsRequired().HasMaxLength(100);
        builder.Entity<PendingRegistration>().Property(registration => registration.Country).IsRequired().HasMaxLength(100);
        builder.Entity<PendingRegistration>().Property(registration => registration.ContactEmail).IsRequired().HasMaxLength(150);
        builder.Entity<PendingRegistration>().Property(registration => registration.Phone).HasMaxLength(40);
        builder.Entity<PendingRegistration>().Property(registration => registration.Category).HasMaxLength(120);
        builder.Entity<PendingRegistration>().Property(registration => registration.ExpiresAt).IsRequired();
        builder.Entity<PendingRegistration>().HasIndex(registration => registration.Email);
        builder.Entity<PendingRegistration>().HasIndex(registration => registration.PublicId).IsUnique();
        builder.Entity<PendingRegistration>().HasIndex(registration => registration.StripeCheckoutSessionId).IsUnique();
        builder.Entity<PendingRegistration>().HasIndex(registration => registration.StripeSubscriptionId);

        builder.Entity<Subscription>().ToTable("Subscriptions");
        builder.Entity<Subscription>().HasKey(subscription => subscription.Id);
        builder.Entity<Subscription>().Property(subscription => subscription.Id).ValueGeneratedOnAdd();
        builder.Entity<Subscription>().Property(subscription => subscription.UserId).IsRequired();
        builder.Entity<Subscription>().Property(subscription => subscription.Role).IsRequired().HasMaxLength(30);
        builder.Entity<Subscription>().Property(subscription => subscription.PlanCode).HasConversion<string>().IsRequired().HasMaxLength(30);
        builder.Entity<Subscription>().Property(subscription => subscription.Status).HasConversion<string>().IsRequired().HasMaxLength(30);
        builder.Entity<Subscription>().Property(subscription => subscription.StripeCustomerId).IsRequired().HasMaxLength(120);
        builder.Entity<Subscription>().Property(subscription => subscription.StripeSubscriptionId).IsRequired().HasMaxLength(120);
        builder.Entity<Subscription>().Property(subscription => subscription.StripePriceId).IsRequired().HasMaxLength(120);
        builder.Entity<Subscription>().HasIndex(subscription => subscription.UserId).IsUnique();
        builder.Entity<Subscription>().HasIndex(subscription => subscription.StripeSubscriptionId).IsUnique();

        builder.Entity<ProcessedWebhookEvent>().ToTable("ProcessedWebhookEvents");
        builder.Entity<ProcessedWebhookEvent>().HasKey(processedEvent => processedEvent.Id);
        builder.Entity<ProcessedWebhookEvent>().Property(processedEvent => processedEvent.Id).ValueGeneratedOnAdd();
        builder.Entity<ProcessedWebhookEvent>().Property(processedEvent => processedEvent.StripeEventId).IsRequired().HasMaxLength(120);
        builder.Entity<ProcessedWebhookEvent>().Property(processedEvent => processedEvent.EventType).IsRequired().HasMaxLength(80);
        builder.Entity<ProcessedWebhookEvent>().Property(processedEvent => processedEvent.ProcessedAt).IsRequired();
        builder.Entity<ProcessedWebhookEvent>().HasIndex(processedEvent => processedEvent.StripeEventId).IsUnique();
    }
}
