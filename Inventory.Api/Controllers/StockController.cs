using Inventory.Application.DTOs;
using Inventory.Application.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly RegisterStockEntryHandler _entryHandler;
        private readonly RegisterStockExitHandler _exitHandler;
        private readonly GetStockByWarehouseHandler _getStockHandler;

        public StockController(
            RegisterStockEntryHandler entryHandler,
            RegisterStockExitHandler exitHandler,
            GetStockByWarehouseHandler getStockHandler)
        {
            _entryHandler = entryHandler;
            _exitHandler = exitHandler;
            _getStockHandler = getStockHandler;
        }

        [HttpPost("entry")]
        public async Task<IActionResult> RegisterEntry([FromBody] StockMovementDto dto)
        {
            await _entryHandler.Handle(dto.WarehouseId, dto.Sku, dto.Quantity, dto.Reference);
            return Ok(new { message = "Entrada registrada correctamente." });
        }

        [HttpPost("exit")]
        public async Task<IActionResult> RegisterExit([FromBody] StockMovementDto dto)
        {
            await _exitHandler.Handle(dto.WarehouseId, dto.Sku, dto.Quantity, dto.Reference);
            return Ok(new { message = "Salida registrada correctamente." });
        }

        [HttpGet("{warehouseId:guid}")]
        public async Task<IActionResult> GetStock(Guid warehouseId)
        {
            var stock = await _getStockHandler.Handle(warehouseId);
            return Ok(stock);
        }
    }
}
