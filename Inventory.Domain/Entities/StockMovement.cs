using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{
    public class StockMovement
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public Guid WarehouseId { get; private set; }
        public string MovementType { get; private set; } = null!; // 'IN' | 'OUT' | 'TRANSFER_IN' | 'TRANSFER_OUT'
        public int Quantity { get; private set; }
        public string? Reference { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        // Navegación
        public Product Product { get; private set; } = null!;
        public Warehouse Warehouse { get; private set; } = null!;

        private StockMovement() { }

        public StockMovement(Guid productId, Guid warehouseId, string movementType, int quantity, string? reference = null)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            WarehouseId = warehouseId;
            MovementType = movementType;
            Quantity = quantity;
            Reference = reference;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
