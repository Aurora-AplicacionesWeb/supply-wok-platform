namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

public record DishResource(int Id, string Code, string Name, int Quantity, string Description, double Price,
    bool Active, bool Outstanding, int DishCategoryId, string? DishCategoryName);
