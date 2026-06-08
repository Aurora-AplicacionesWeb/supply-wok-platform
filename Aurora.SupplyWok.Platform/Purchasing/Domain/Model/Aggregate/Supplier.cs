namespace Aurora.SupplyWok.Platform.Purchasing.Domain.Model.Aggregate;

/// <summary>
/// Minimal supplier projection required by purchase orders.
/// </summary>
public class Supplier
{
    public Supplier()
    {
        Name = string.Empty;
        ContactName = string.Empty;
        Email = string.Empty;
        Phone = string.Empty;
        Category = string.Empty;
        LinkedDate = string.Empty;
        Sla = string.Empty;
        ResponseTime = string.Empty;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public string ContactName { get; private set; }

    public string Email { get; private set; }

    public string Phone { get; private set; }

    public string Category { get; private set; }

    public string LinkedDate { get; private set; }

    public string Sla { get; private set; }

    public string ResponseTime { get; private set; }
}
