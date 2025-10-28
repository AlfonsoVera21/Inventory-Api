using Inventory.Domain.Entities;
using Inventory.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.UseCases
{
    public class GetStockByWarehouseHandler
    {
        private readonly IStockRepository _stockRepository;

        public GetStockByWarehouseHandler(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<IEnumerable<StockItem>> Handle(Guid warehouseId)
        {
            return await _stockRepository.GetStockByWarehouseAsync(warehouseId);
        }
    }
}
