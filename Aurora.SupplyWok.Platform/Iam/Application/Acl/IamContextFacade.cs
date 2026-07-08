using Aurora.SupplyWok.Platform.Iam.Application.QueryServices;
using Aurora.SupplyWok.Platform.Iam.Domain.Model.Aggregates;
using Aurora.SupplyWok.Platform.Iam.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iam.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Iam.Domain.Repositories;
using Aurora.SupplyWok.Platform.Iam.Interfaces.Acl;
using Aurora.SupplyWok.Platform.Shared.Domain.Repositories;

namespace Aurora.SupplyWok.Platform.Iam.Application.Acl;

public class IamContextFacade(
    IUserRepository userRepository,
    IUserQueryService userQueryService,
    IUnitOfWork unitOfWork)
    : IIamContextFacade
{
    public async Task<int> CreateUser(string email, string passwordHash, string role,
        CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(email, cancellationToken)) return 0;

        var user = new User(email, passwordHash, role);
        await userRepository.AddAsync(user, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var getUserByEmailQuery = new GetUserByEmailQuery(email);
        var result = await userQueryService.Handle(getUserByEmailQuery, cancellationToken);
        return result?.Id ?? 0;
    }

    public async Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken)
    {
        var getUserByEmailQuery = new GetUserByEmailQuery(email);
        var result = await userQueryService.Handle(getUserByEmailQuery, cancellationToken);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery, cancellationToken);
        return result?.Email ?? string.Empty;
    }
}
