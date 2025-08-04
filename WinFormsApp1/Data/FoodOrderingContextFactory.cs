using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Data/FoodOrderingContextFactory.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FoodOrderingSystem.Data
{
    public class FoodOrderingContextFactory : IDesignTimeDbContextFactory<FoodOrderingContext>
    {
        public FoodOrderingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderingContext>();

            // Use the SAME server name that works in SSMS
            optionsBuilder.UseSqlServer(@"Server=.;Database=FoodOrderingSystemDB;Integrated Security=true;TrustServerCertificate=true;");

            return new FoodOrderingContext(optionsBuilder.Options);
        }
    }
}