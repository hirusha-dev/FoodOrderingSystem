// Data/FoodOrderingContext.cs
using Microsoft.EntityFrameworkCore;
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Data
{
    public class FoodOrderingContext : DbContext
    {
        public FoodOrderingContext(DbContextOptions<FoodOrderingContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Waiter> Waiters { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure User entity
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configure Waiter relationship
            modelBuilder.Entity<Waiter>()
                .HasOne(w => w.User)
                .WithOne()
                .HasForeignKey<Waiter>(w => w.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Chef relationship
            modelBuilder.Entity<Chef>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Chef>(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Order relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Waiter)
                .WithMany(w => w.Orders)
                .HasForeignKey(o => o.WaiterID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Chef)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ChefID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableID)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure OrderItem relationships
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany(mi => mi.OrderItems)
                .HasForeignKey(oi => oi.ItemID)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed initial data
            SeedData(modelBuilder);
        }

        // Data/FoodOrderingContext.cs - REPLACE the SeedData method
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Use static dates instead of DateTime.Now
            var staticDate = new DateTime(2025, 1, 1, 9, 0, 0);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { UserID = 1, Name = "Admin User", Username = "admin", Password = "admin123", Role = "Manager", CreatedDate = staticDate, IsActive = true },
                new User { UserID = 2, Name = "John Waiter", Username = "waiter1", Password = "waiter123", Role = "Waiter", CreatedDate = staticDate, IsActive = true },
                new User { UserID = 3, Name = "Maria Chef", Username = "chef1", Password = "chef123", Role = "Chef", CreatedDate = staticDate, IsActive = true },
                new User { UserID = 4, Name = "Alice Waiter", Username = "waiter2", Password = "waiter123", Role = "Waiter", CreatedDate = staticDate, IsActive = true }, 
                new User { UserID = 5, Name = "Carlos Chef", Username = "chef2", Password = "chef123", Role = "Chef", CreatedDate = staticDate, IsActive = true }      
            );

            // Seed Waiters
            modelBuilder.Entity<Waiter>().HasData(
                new Waiter { UserID = 2, Specialty = "Customer Service", ShiftTime = "Day Shift" },
                new Waiter { UserID = 4, Specialty = "Outdoor Tables", ShiftTime = "Evening Shift" }
            );

            // Seed Chefs
            modelBuilder.Entity<Chef>().HasData(
                new Chef { UserID = 3, Specialty = "Italian Cuisine", ShiftTime = "Day Shift" },
                new Chef { UserID = 5, Specialty = "Seafood", ShiftTime = "Night Shift" }
            );

            // Seed Tables
            modelBuilder.Entity<Table>().HasData(
                new Table { TableID = 1, Capacity = 2, Status = "Available" },
                new Table { TableID = 2, Capacity = 4, Status = "Available" },
                new Table { TableID = 3, Capacity = 6, Status = "Available" },
                new Table { TableID = 4, Capacity = 4, Status = "Available" },
                new Table { TableID = 5, Capacity = 2, Status = "Available" }
            );

            // Seed Menu Items
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem { ItemID = 1, Name = "Margherita Pizza", Description = "Classic pizza with tomato sauce and mozzarella", Price = 12.99m, Category = "Pizza", IsAvailable = true },
                new MenuItem { ItemID = 2, Name = "Chicken Pasta", Description = "Creamy pasta with grilled chicken", Price = 14.99m, Category = "Pasta", IsAvailable = true },
                new MenuItem { ItemID = 3, Name = "Caesar Salad", Description = "Fresh romaine lettuce with caesar dressing", Price = 8.99m, Category = "Salad", IsAvailable = true },
                new MenuItem { ItemID = 4, Name = "Grilled Salmon", Description = "Fresh salmon with herbs and lemon", Price = 18.99m, Category = "Main Course", IsAvailable = true },
                new MenuItem { ItemID = 5, Name = "Chocolate Cake", Description = "Rich chocolate cake with frosting", Price = 6.99m, Category = "Dessert", IsAvailable = true }
            );
        }
    }
}
// Note: Ensure you have the necessary using directives for Entity Framework Core and your models.