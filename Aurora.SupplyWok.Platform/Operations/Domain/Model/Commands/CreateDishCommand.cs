namespace Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;

public record CreateDishCommand(
    string Code,
    string Name,
    int Quantity,
    string Description,
    double Price,
    bool Active,
    bool Outstanding,
    int DishCategoryId);
