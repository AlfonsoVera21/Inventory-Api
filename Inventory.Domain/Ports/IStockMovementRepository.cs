using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Ports
{
    public interface IStockMovementRepository
    {
        Task AddAsync(StockMovement movement);
        Task<IEnumerable<StockMovement>> GetMovementsByProductAsync(Guid productId);
    }
}
