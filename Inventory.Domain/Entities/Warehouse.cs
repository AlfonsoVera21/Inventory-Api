using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class Warehouse
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string? Location { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // Relaciones
        public ICollection<StockItem> StockItems { get; private set; } = new List<StockItem>();
        public ICollection<StockMovement> Movements { get; private set; } = new List<StockMovement>();

        private Warehouse() { }

        public Warehouse(string name, string? location = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Location = location;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Desactivate() => IsActive = false;
    }
}
