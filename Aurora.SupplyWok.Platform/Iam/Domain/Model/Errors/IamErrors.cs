using Aurora.SupplyWok.Platform.Shared.Domain.Model;

namespace Aurora.SupplyWok.Platform.Iam.Domain.Model.Errors;

public static class IamErrors
{
    public static readonly Error InvalidCredentials = new("Iam.InvalidCredentials", "Invalid email or password.");

    public static readonly Error EmailAlreadyTaken =
        new("Iam.EmailAlreadyTaken", "The specified email is already taken.");

    public static readonly Error UserCreationFailed =
        new("Iam.UserCreationFailed", "An error occurred while creating the user.");
}