namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

/// <summary>
/// Partial content class for the <see cref="RestaurantReference"/> aggregate.
/// </summary>
public partial class RestaurantReference
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RestaurantReference"/> aggregate with default values.
    /// </summary>
    public RestaurantReference()
    {
        Name = string.Empty;
        District = string.Empty;
        Status = string.Empty;
    }
}
