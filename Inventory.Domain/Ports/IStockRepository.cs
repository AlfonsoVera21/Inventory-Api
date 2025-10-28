using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Ports
{
    public interface IStockRepository
    {
        Task<StockItem?> GetStockItemAsync(Guid productId, Guid warehouseId);
        Task<IEnumerable<StockItem>> GetStockByWarehouseAsync(Guid warehouseId);
        Task AddAsync(StockItem item);
        Task UpdateAsync(StockItem item);
    }
}
