using Aurora.SupplyWok.Platform.Iam.Application.CommandServices;
using Aurora.SupplyWok.Platform.Iam.Application.QueryServices;
using Aurora.SupplyWok.Platform.Iam.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Iam.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Iam.Interfaces.Acl;

namespace Aurora.SupplyWok.Platform.Iam.Application.Acl;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService)
    : IIamContextFacade
{
    public async Task<int> CreateUser(string email, string password, CancellationToken cancellationToken)
    {
        var signUpCommand = new SignUpCommand(email, password);
        var signUpResult = await userCommandService.Handle(signUpCommand, cancellationToken);
        if (signUpResult.IsFailure) return 0;
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