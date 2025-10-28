using Inventory.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Services
{
    public class StockService
    {
        private readonly RegisterStockEntryHandler _entryHandler;
        private readonly RegisterStockExitHandler _exitHandler;
        private readonly GetStockByWarehouseHandler _getStockHandler;

        public StockService(
            RegisterStockEntryHandler entryHandler,
            RegisterStockExitHandler exitHandler,
            GetStockByWarehouseHandler getStockHandler)
        {
            _entryHandler = entryHandler;
            _exitHandler = exitHandler;
            _getStockHandler = getStockHandler;
        }

        public async Task RegisterEntry(Guid warehouseId, string sku, int quantity, string? reference = null) {
            await _entryHandler.Handle(warehouseId, sku, quantity, reference);
        }

        public async Task RegisterExit(Guid warehouseId, string sku, int quantity, string? reference = null) {
            await _exitHandler.Handle(warehouseId, sku, quantity, reference);
        }

        public async Task<Object> GetStockByWarehouse(Guid warehouseId) {
            var items = await _getStockHandler.Handle(warehouseId);
            return items.Select(i => new
            {
                i.ProductId,
                i.Product.Sku,
                i.Product.Name,
                i.Quantity
            });
        }
    }
}
