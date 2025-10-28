using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Ports
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> GetByIdAsync(Guid id);
        Task<IEnumerable<Warehouse>> GetAllAsync();
        Task AddAsync(Warehouse warehouse);
        Task UpdateAsync(Warehouse warehouse);
        Task DeleteAsync(Guid id);
    }
}
