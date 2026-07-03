using System.Net.Mime;
using Aurora.SupplyWok.Platform.Inventory.Application.CommandServices;
using Aurora.SupplyWok.Platform.Inventory.Application.QueryServices;
using Aurora.SupplyWok.Platform.Inventory.Domain.Model.Queries;
using Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest.Resources;
using Aurora.SupplyWok.Platform.Inventory.Resources;
using Aurora.SupplyWok.Platform.Shared.Resources.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Swashbuckle.AspNetCore.Annotations;

namespace Aurora.SupplyWok.Platform.Inventory.Interfaces.Rest;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Inventory Transaction Endpoints.")]
public class InventoryTransactionsController(
    IInventoryTransactionCommandService inventoryTransactionCommandService,
    IInventoryTransactionQueryService inventoryTransactionQueryService,
    IStringLocalizer<InventoryMessages> inventoryMessagesLocalizer,
    IStringLocalizer<ErrorMessages> errorLocalizer) : ControllerBase
{
    private readonly IStringLocalizer<InventoryMessages> _inventoryMessagesLocalizer = inventoryMessagesLocalizer;
    private readonly IStringLocalizer<ErrorMessages> _errorLocalizer = errorLocalizer;
    [HttpPost]
    [SwaggerOperation("Create Inventory Transaction", "Creates a new inventory transaction with its operations.", OperationId = "CreateInventoryTransaction")]
    [SwaggerResponse(201, "The inventory transaction was created successfully.", typeof(InventoryTransactionResource))]
    [SwaggerResponse(400, "Invalid request.")]
    public async Task<IActionResult> CreateInventoryTransaction(
        [FromBody] CreateInventoryTransactionResource resource,
        CancellationToken cancellationToken)
    {
        var command = Transform.CreateInventoryTransactionCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await inventoryTransactionCommandService.Handle(command, cancellationToken);

        if (!result.IsSuccess) return ToFailureResponse(result.Error, result.Message);

        var transactionResource = Transform.InventoryTransactionResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetInventoryTransactionById), new { inventoryTransactionId = transactionResource.Id }, transactionResource);
    }

    [HttpGet]
    [SwaggerOperation("Get All Inventory Transactions", "Gets all inventory transactions.", OperationId = "GetAllInventoryTransactions")]
    [SwaggerResponse(200, "Inventory transactions retrieved successfully.", typeof(IEnumerable<InventoryTransactionResource>))]
    public async Task<IActionResult> GetAllInventoryTransactions(CancellationToken cancellationToken)
    {
        var query = new GetAllInventoryTransactionsQuery();
        var transactions = await inventoryTransactionQueryService.Handle(query, cancellationToken);
        var resources = transactions.Select(Transform.InventoryTransactionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("{inventoryTransactionId:int}")]
    [SwaggerOperation("Get Inventory Transaction by Id", "Gets an inventory transaction by its unique identifier.", OperationId = "GetInventoryTransactionById")]
    [SwaggerResponse(200, "The inventory transaction was found and returned.", typeof(InventoryTransactionResource))]
    [SwaggerResponse(404, "The inventory transaction was not found.")]
    public async Task<IActionResult> GetInventoryTransactionById(int inventoryTransactionId, CancellationToken cancellationToken)
    {
        var query = new GetInventoryTransactionByIdQuery(inventoryTransactionId);
        var transaction = await inventoryTransactionQueryService.Handle(query, cancellationToken);

        if (transaction is null) return NotFound();
        return Ok(Transform.InventoryTransactionResourceFromEntityAssembler.ToResourceFromEntity(transaction));
    }

    //[HttpGet("supplies/{supplyId:int}/inventory-transactions")]
    [HttpGet("~/api/v1/supplies/{supplyId:int}/inventory-transactions")]
    [SwaggerOperation("Get Inventory Transactions by Supply Id", "Gets inventory transactions by supply identifier.", OperationId = "GetInventoryTransactionsBySupplyId")]
    [SwaggerResponse(200, "Inventory transactions retrieved successfully.", typeof(IEnumerable<InventoryTransactionResource>))]
    public async Task<IActionResult> GetInventoryTransactionsBySupplyId(int supplyId, CancellationToken cancellationToken)
    {
        var query = new GetInventoryTransactionsBySupplyIdQuery(supplyId);
        var transactions = await inventoryTransactionQueryService.Handle(query, cancellationToken);
        var resources = transactions.Select(Transform.InventoryTransactionResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    private static IActionResult ToFailureResponse(Enum? error, string message)
    {
        return Transform.InventoryActionResultAssembler.ToFailureResponse(error, message);
    }
}
