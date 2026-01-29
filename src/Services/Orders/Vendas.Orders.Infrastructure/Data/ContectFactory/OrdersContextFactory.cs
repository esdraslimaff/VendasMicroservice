using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Orders.Infrastructure.Data.Context;

namespace Vendas.Orders.Infrastructure.Data.ContectFactory
{
    public class OrdersContextFactory : IDesignTimeDbContextFactory<OrdersContext>
    {
        public OrdersContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrdersContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=OrdersDb;User Id=sa;Password=!Senhafacil1234;TrustServerCertificate=True;");

            return new OrdersContext(optionsBuilder.Options);
        }
    }
}
