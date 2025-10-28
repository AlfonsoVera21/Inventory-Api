using Inventory.Application.DTOs;
using Inventory.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _productService.CreateProductAsync(dto.Sku, dto.Name, dto.Description, dto.MinStock);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Devuelve un producto por ID.
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var products = await _productService.GetAllActiveAsync();
            var product = products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound(new { message = "Producto no encontrado." });

            return Ok(product);
        }

        /// <summary>
        /// Devuelve todos los productos activos.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllActiveAsync();
            return Ok(products);
        }

        /// <summary>
        /// Desactiva (elimina lógicamente) un producto.
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Desactivate(Guid id)
        {
            await _productService.DesactivateProductAsync(id);
            return Ok(new { message = "Producto desactivado correctamente." });
        }
    }
}
