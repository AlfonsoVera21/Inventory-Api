using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Sku { get; private set; } = null!;
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public int MinStock { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public ICollection<StockItem> StockItems { get; private set; } = new List<StockItem>();
        public ICollection<StockMovement> Movements { get; private set; } = new List<StockMovement>();
        private Product() { }
        public Product(string sku, string name, string? description = null, int minStock = 0)
        {
            Id = Guid.NewGuid();
            Sku = sku;
            Name = name;
            Description = description;
            MinStock = minStock;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
        public void Desactivate() => IsActive = false;
    }
}
