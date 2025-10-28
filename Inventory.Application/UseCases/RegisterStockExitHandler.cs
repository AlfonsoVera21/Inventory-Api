using Inventory.Domain.Entities;
using Inventory.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.UseCases
{
    public class RegisterStockExitHandler
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IStockMovementRepository _stockMovementRepository;

        public RegisterStockExitHandler(IProductRepository productRepository, IStockRepository stockRepository, IStockMovementRepository stockMovementRepository)
        {
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _stockMovementRepository = stockMovementRepository;
        }

        public async Task Handle(Guid warehouseId, string sku, int quantity, string? reference = null)
        {
            var product = await _productRepository.GetBySkuAsync(sku);
            if (product == null || !product.IsActive) throw new Exception($"El producto con SKU '{sku}' no existe o esta inactivo.");
            
            var stock = await _stockRepository.GetStockItemAsync(product.Id, warehouseId);
            if (stock is null || stock.Quantity < quantity) throw new Exception($"No hay suficiente stock para el producto con SKU '{sku}' en el almacén '{warehouseId}'.");

            stock.Subtract(quantity);
            await _stockRepository.UpdateAsync(stock);

            var movement = new StockMovement(product.Id, warehouseId, "OUT", quantity, reference);
            await _stockMovementRepository.AddAsync(movement);
        }
    }
}
