using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;

        public WarehousesController(WarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        /// <summary>
        /// Devuelve todos los productos dentro de una bodega.
        /// </summary>
        [HttpGet("{warehouseId:guid}/products")]
        public async Task<IActionResult> GetProducts(Guid warehouseId)
        {
            var products = await _warehouseService.GetProductsInWarehouse(warehouseId);
            return Ok(products);
        }

        /// <summary>
        /// Devuelve el stock total de un producto sumando todas las bodegas.
        /// </summary>
        [HttpGet("product/{productId:guid}/total")]
        public async Task<IActionResult> GetTotalStock(Guid productId)
        {
            var total = await _warehouseService.GetTotalStock(productId);
            return Ok(new { productId, total });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WarehouseDto dto)
        {
            var warehouse = await _warehouseService.CreateAsync(dto.Name, dto.Location);
            return Ok(warehouse);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _warehouseService.GetAllActiveAsync();
            return Ok(products);
        }
    }
}
