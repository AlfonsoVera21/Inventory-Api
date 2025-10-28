using Inventory.Application.Services;
using Inventory.Application.UseCases;
using Inventory.Domain.Ports;
using Inventory.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.DependencyInjection
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();

            services.AddScoped<RegisterStockEntryHandler>();
            services.AddScoped<RegisterStockExitHandler>();
            services.AddScoped<GetStockByWarehouseHandler>();

            services.AddScoped<WarehouseService>();
            services.AddScoped<StockService>();
            services.AddScoped<ProductService>();
            return services;
        }
    }
}
