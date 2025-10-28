using Inventory.Domain.Entities;
using Inventory.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> CreateProductAsync(string sku, string name, string? description, int minStock)
        {
            var existing = await _productRepository.GetBySkuAsync(sku);
            if (existing != null) throw new Exception($"Ya existe un producto con el sku '{sku}'.");
            var product = new Product(sku, name, description, minStock);
            await _productRepository.AddAsync(product);
            return product;
        }
        public async Task<IEnumerable<Product>> GetAllActiveAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task DesactivateProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new Exception("Producto no encontrado.");

            product.Desactivate();
            await _productRepository.UpdateAsync(product);
        }
    }
}
