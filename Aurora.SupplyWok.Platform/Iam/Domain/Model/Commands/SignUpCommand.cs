namespace Aurora.SupplyWok.Platform.Iam.Domain.Model.Commands;

/**
 * <summary>
 *     The sign up command
 * </summary>
 * <remarks>
 *     This command object includes the Email and password to sign up
 * </remarks>
 */
public record SignUpCommand(string Email, string Password);