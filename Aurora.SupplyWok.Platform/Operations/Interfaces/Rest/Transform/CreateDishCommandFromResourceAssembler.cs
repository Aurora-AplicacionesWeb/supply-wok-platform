using Aurora.SupplyWok.Platform.Operations.Domain.Model.Commands;
using Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Resources;

namespace Aurora.SupplyWok.Platform.Operations.Interfaces.Rest.Transform;

public static class CreateDishCommandFromResourceAssembler
{
    public static CreateDishCommand ToCommandFromResource(CreateDishResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource));

        return new CreateDishCommand(
            resource.Code,
            resource.Name,
            resource.Quantity,
            resource.Description,
            resource.Price,
            resource.Active,
            resource.Outstanding,
            resource.DishCategoryId);
    }
}
