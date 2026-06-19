namespace Aurora.SupplyWok.Platform.Profiles.Domain.Model.Aggregates;

/// <summary>
/// Partial class for the <see cref="RestaurantProfile"/> aggregate
/// </summary>
public partial class RestaurantProfile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RestaurantProfile"/> class with default values.
    /// </summary>
    public RestaurantProfile()
    {
        BusinessName = string.Empty;
        Status = "Active";
    }
}
