using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Data/DatabaseService.cs - REPLACE THE ENTIRE FILE
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Data
{
    public static class DatabaseService
    {
        private static string GetConnectionString()
        {
            // SQL Server connection string for SSMS
            // Replace "YOUR_SERVER_NAME" with your actual SQL Server instance name
            // Common examples:
            // - "localhost" or "." (if SQL Server is on same machine)
            // - "YOUR_COMPUTER_NAME\SQLEXPRESS" (for SQL Server Express)
            // - "YOUR_COMPUTER_NAME\MSSQLSERVER" (for full SQL Server)

            return @"Server=.;Database=FoodOrderingSystemDB;Integrated Security=true;TrustServerCertificate=true;";
        }

        public static FoodOrderingContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FoodOrderingContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());
            return new FoodOrderingContext(optionsBuilder.Options);
        }

        public static async Task InitializeDatabaseAsync()
        {
            using var context = CreateContext();

            try
            {
                // Create database if it doesn't exist
                await context.Database.EnsureCreatedAsync();

                Console.WriteLine("Database initialized successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                throw;
            }
        }
    }
}