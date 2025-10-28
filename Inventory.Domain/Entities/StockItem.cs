using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class StockItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid WarehouseId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        // Navegación
        public Product Product { get; private set; } = null!;
        public Warehouse Warehouse { get; private set; } = null!;

        private StockItem() { }

        public StockItem(Guid productId, Guid warehouseId, int quantity = 0)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            WarehouseId = warehouseId;
            Quantity = quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Add(int qty)
        {
            Quantity += qty;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Subtract(int qty)
        {
            if (qty > Quantity)
                throw new InvalidOperationException("No hay stock suficiente.");
            Quantity -= qty;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
