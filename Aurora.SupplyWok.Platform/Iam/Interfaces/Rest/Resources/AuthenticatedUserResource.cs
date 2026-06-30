namespace Aurora.SupplyWok.Platform.Iam.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(int Id, string Email, string Token);