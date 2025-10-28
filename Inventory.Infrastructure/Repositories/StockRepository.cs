using Inventory.Domain.Entities;
using Inventory.Domain.Ports;
using Inventory.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly InventoryDbContext _context;
        public StockRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<StockItem?> GetStockItemAsync(Guid productId, Guid warehouseId) {
            return await _context.StockItems.FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId);
        }

        public async Task<IEnumerable<StockItem>> GetStockByWarehouseAsync(Guid warehouseId)
        {
            return await _context.StockItems
                .Include(s => s.Product)
                .Where(s => s.WarehouseId == warehouseId)
                .ToListAsync();
        }

        public async Task AddAsync(StockItem item)
        {
            await _context.StockItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StockItem item)
        {
            _context.StockItems.Update(item);
            await _context.SaveChangesAsync();
        }
    }
}
