using Inventory.Domain.Entities;
using Inventory.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class WarehouseService
    {
        private readonly IStockRepository _stockRepository;
        private readonly IProductRepository _productRepository;
        private readonly IWarehouseRepository _warehouseRepository;
        public WarehouseService(IProductRepository productRepository, IStockRepository stockRepository, IWarehouseRepository warehouseRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<object>> GetProductsInWarehouse(Guid warehouseId)
        {
            var stockItems = await _stockRepository.GetStockByWarehouseAsync(warehouseId);

            return stockItems.Select(s => new
            {
                s.ProductId,
                s.Product.Name,
                s.Product.Sku,
                s.Quantity
            });
        }
        public async Task<int> GetTotalStock(Guid productId)
        {
            var warehouses = await _stockRepository.GetStockByWarehouseAsync(Guid.Empty);
            return warehouses
                .Where(w => w.ProductId == productId)
                .Sum(w => w.Quantity);
        }
        public async Task<Warehouse> CreateAsync(string name, string? location)
        {
            var warehouse = new Warehouse(name, location);
            await _warehouseRepository.AddAsync(warehouse);
            return warehouse;
        }

        public async Task<IEnumerable<Warehouse>> GetAllActiveAsync()
        {
            return await _warehouseRepository.GetAllAsync();
        }
    }
}
