using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Orders.Domain.Entities;

namespace Vendas.Orders.Infrastructure.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.Items)
                   .WithOne()
                   .HasForeignKey("OrderId")
                   .OnDelete(DeleteBehavior.Cascade); 

            builder.Property(o => o.Status)
                   .HasConversion<int>()
                   .IsRequired();
        }
    }
}
