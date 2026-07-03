namespace Aurora.SupplyWok.Platform.Iam.Interfaces.Rest.Resources;

public record SignUpResource(string Email, string Password, string Role = "restaurant");