namespace Aurora.SupplyWok.Platform.Spm.Domain.Model.Aggregates;

/// <summary>
///     Supplier-client link aggregate for the Suppliers bounded context.
/// </summary>
/// <remarks>
///     This aggregate represents the explicit relationship between a supplier profile and a restaurant profile.
/// </remarks>
public partial class SupplierRestaurant
{
    /// <summary>
    ///     Initializes a new supplier-client link instance with business data.
    /// </summary>
    /// <param name="supplierProfileId">The supplier profile identifier.</param>
    /// <param name="restaurantProfileId">The restaurant profile identifier.</param>
    /// <param name="linkedDate">The date when the relationship was established.</param>
    /// <param name="status">The current relationship status.</param>
    /// <param name="sla">The supplier SLA in this relationship.</param>
    /// <param name="responseTime">The supplier response time in this relationship.</param>
    public SupplierRestaurant(
        int supplierProfileId,
        int restaurantProfileId,
        string linkedDate,
        string status,
        string sla,
        string responseTime)
    {
        if (supplierProfileId <= 0)
            throw new ArgumentException("Supplier profile id must be greater than zero.", nameof(supplierProfileId));
        if (restaurantProfileId <= 0)
            throw new ArgumentException("Restaurant profile id must be greater than zero.", nameof(restaurantProfileId));
        if (string.IsNullOrWhiteSpace(linkedDate))
            throw new ArgumentException("Linked date cannot be empty.", nameof(linkedDate));
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Relationship status cannot be empty.", nameof(status));
        if (string.IsNullOrWhiteSpace(sla))
            throw new ArgumentException("SLA cannot be empty.", nameof(sla));
        if (string.IsNullOrWhiteSpace(responseTime))
            throw new ArgumentException("Response time cannot be empty.", nameof(responseTime));

        SupplierProfileId = supplierProfileId;
        RestaurantProfileId = restaurantProfileId;
        LinkedDate = linkedDate.Trim();
        Status = status.Trim();
        Sla = sla.Trim();
        ResponseTime = responseTime.Trim();
    }

    /// <summary>
    ///     Gets the supplier-client link identifier.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    ///     Gets the related supplier profile identifier.
    /// </summary>
    public int SupplierProfileId { get; private set; }

    /// <summary>
    ///     Gets the related restaurant profile identifier.
    /// </summary>
    public int RestaurantProfileId { get; private set; }

    public string LinkedDate { get; private set; }

    public string Status { get; private set; }

    public string Sla { get; private set; }

    public string ResponseTime { get; private set; }
}
