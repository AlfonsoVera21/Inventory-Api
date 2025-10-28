using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Persistence.Configurations
{
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("stock_movements");

            builder.HasKey(m => m.Id);

            builder.HasOne(m => m.Product)
                .WithMany(p => p.Movements)
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.Warehouse)
                .WithMany(w => w.Movements)
                .HasForeignKey(m => m.WarehouseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(m => m.MovementType)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(m => m.Quantity)
                .IsRequired();

            builder.Property(m => m.Reference)
                .HasMaxLength(100);

            builder.Property(m => m.CreatedAt)
                .HasDefaultValueSql("NOW()");
        }
    }
}
