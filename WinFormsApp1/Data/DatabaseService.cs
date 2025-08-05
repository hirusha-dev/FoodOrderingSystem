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
                Console.WriteLine("Initializing database...");

                // Test connection first
                bool canConnect = await context.Database.CanConnectAsync();
                Console.WriteLine($"Database connection test: {(canConnect ? "SUCCESS" : "FAILED")}");

                if (!canConnect)
                {
                    Console.WriteLine("Attempting to create database...");
                }

                // Create database if it doesn't exist
                bool created = await context.Database.EnsureCreatedAsync();
                Console.WriteLine($"Database creation: {(created ? "CREATED NEW" : "ALREADY EXISTS")}");

                // Apply any pending migrations
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                {
                    Console.WriteLine($"Applying {pendingMigrations.Count()} pending migrations...");
                    await context.Database.MigrateAsync();
                }

                // Test with a simple query
                var userCount = await context.Users.CountAsync();
                Console.WriteLine($"Database initialized successfully! Users in database: {userCount}");

                // Verify seed data
                if (userCount == 0)
                {
                    Console.WriteLine("WARNING: No users found in database. Seed data may not have been created.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing database: {ex.Message}");
                Console.WriteLine($"Connection string: {GetConnectionString()}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }
    }
}