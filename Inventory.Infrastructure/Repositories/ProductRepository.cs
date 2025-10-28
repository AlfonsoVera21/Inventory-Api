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
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;
        public ProductRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product) {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id) {
            var entity = await _context.Products.FindAsync(id);
            if (entity != null) {
                entity.Desactivate();
                _context.Products.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync() {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetBySkuAsync(string sku) {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Sku == sku);
        }

        public async Task UpdateAsync(Product product) {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }


    }
}
