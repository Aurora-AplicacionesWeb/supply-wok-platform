namespace Aurora.SupplyWok.Platform.Iam.Interfaces.Acl;

public interface IIamContextFacade
{
    Task<int> CreateUser(string email, string password, CancellationToken cancellationToken);
    Task<int> FetchUserIdByEmail(string email, CancellationToken cancellationToken);
    Task<string> FetchEmailByUserId(int userId, CancellationToken cancellationToken);
}