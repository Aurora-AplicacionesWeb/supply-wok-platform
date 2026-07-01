namespace Aurora.SupplyWok.Platform.Iam.Domain.Model.Commands;

/**
 * <summary>
 *     The sign in command
 * </summary>
 * <remarks>
 *     This command object includes the email and password to sign in
 * </remarks>
 */
public record SignInCommand(string Email, string Password);