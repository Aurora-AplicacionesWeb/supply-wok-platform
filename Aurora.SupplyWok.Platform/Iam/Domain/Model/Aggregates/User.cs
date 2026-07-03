using System.Text.Json.Serialization;

namespace Aurora.SupplyWok.Platform.Iam.Domain.Model.Aggregates;

/**
 * <summary>
 *     The user aggregate
 * </summary>
 * <remarks>
 *     This class is used to represent a user
 * </remarks>
 */
public partial class User(string email, string passwordHash, string role = "restaurant")
{
    public User() : this(string.Empty, string.Empty, "restaurant")
    {
    }

    public int Id { get; }
    public string Email { get; private set; } = email;

    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;

    public string Role { get; private set; } = role;

    /**
     * <summary>
     *     Update the email
     * </summary>
     * <param name="email">The new email</param>
     * <returns>The updated user</returns>
     */
    public User UpdateEmail(string email)
    {
        Email = email;    
        return this;
    }

    /**
     * <summary>
     *     Update the password hash
     * </summary>
     * <param name="passwordHash">The new password hash</param>
     * <returns>The updated user</returns>
     */
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
}