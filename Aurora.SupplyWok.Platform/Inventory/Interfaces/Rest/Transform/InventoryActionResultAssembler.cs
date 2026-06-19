using Aurora.SupplyWok.Platform.Inventory.Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Transform;

/// <summary>
/// Assembles action results from inventory operation results.
/// </summary>
public static class InventoryActionResultAssembler
{
    /// <summary>
    /// Converts an <see cref="InventoryError"/> and message into the appropriate <see cref="IActionResult"/>.
    /// </summary>
    /// <param name="error">The domain error enum.</param>
    /// <param name="message">The error message.</param>
    /// <returns>The appropriate <see cref="IActionResult"/> based on the error type.</returns>
    public static IActionResult ToFailureResponse(Enum? error, string message)
    {
        if (error is InventoryError err)
        {
            return err switch
            {
                InventoryError.SupplyNotFound => new NotFoundObjectResult(message),
                InventoryError.InventoryTransactionNotFound => new NotFoundObjectResult(message),
                InventoryError.InsufficientStock => new BadRequestObjectResult(message),
                InventoryError.TransferNotSupported => new BadRequestObjectResult(message),
                InventoryError.InvalidData => new BadRequestObjectResult(message),
                InventoryError.OperationCancelled => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                InventoryError.DatabaseError => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                InventoryError.InternalServerError => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                _ => new BadRequestObjectResult(message)
            };
        }

        return new BadRequestObjectResult(message);
    }
}
