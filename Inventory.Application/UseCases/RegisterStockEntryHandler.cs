using Inventory.Domain.Entities;
using Inventory.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.UseCases
{
    public class RegisterStockEntryHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;

        public RegisterStockEntryHandler(IProductRepository productRepository, IStockRepository stockRepository, IStockMovementRepository stockMovementRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task Handle(Guid warehouseId, string sku, int quantity, string? reference = null)
        {
            var product = await _productRepository.GetBySkuAsync(sku);
            if (product == null || !product.IsActive) {
                throw new Exception($"El producto con SKU '{sku}' no existe o esta inactivo.");
            }
            var stock = await _stockRepository.GetStockItemAsync(product.Id, warehouseId);
            if (stock is null)
            {
                stock = new StockItem(product.Id, warehouseId, quantity);
                await _stockRepository.AddAsync(stock);
            }else
            {
                stock.Add(quantity);
                await _stockRepository.UpdateAsync(stock);
            }

            var movement = new StockMovement(product.Id, warehouseId, "IN", quantity, reference);
            await _stockMovementRepository.AddAsync(movement);
        }
    }
}
