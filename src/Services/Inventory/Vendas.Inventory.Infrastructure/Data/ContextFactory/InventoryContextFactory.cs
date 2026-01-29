using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Inventory.Infrastructure.Data.Context;

namespace Vendas.Inventory.Infrastructure.Data.ContextFactory
{
    public class InventoryContextFactory : IDesignTimeDbContextFactory<InventoryContext>
    {
        public InventoryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InventoryContext>();

            optionsBuilder.UseSqlServer("Server=localhost;Database=InventoryDb;User Id=sa;Password=!Senhafacil1234;TrustServerCertificate=True;");

            return new InventoryContext(optionsBuilder.Options);
        }
    }
}
