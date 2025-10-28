using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.DTOs
{
    public class StockMovementDto
    {
        public Guid WarehouseId { get; set; }
        public string Sku { get; set; } = null!;
        public int Quantity { get; set; }
        public string? Reference { get; set; }
    }
}
