using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Services/ChefService.cs
using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Services
{
    public static class ChefService
    {
        public static async Task<Chef?> GetChefByUserIdAsync(int userId)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.Chefs
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.UserID == userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading chef: {ex.Message}");
            }
        }

        public static async Task<bool> EnsureChefExistsAsync(int userId)
        {
            try
            {
                using var context = DatabaseService.CreateContext();

                var chef = await context.Chefs.FindAsync(userId);
                if (chef == null)
                {
                    // Create chef record if it doesn't exist
                    chef = new Chef
                    {
                        UserID = userId,
                        Specialty = "General Cooking",
                        ShiftTime = "Current Shift"
                    };

                    context.Chefs.Add(chef);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ensuring chef exists: {ex.Message}");
            }
        }

        public static async Task<List<Order>> GetPendingOrdersAsync()
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Include(o => o.Table)
                    .Include(o => o.Waiter)
                        .ThenInclude(w => w.User)
                    .Where(o => o.Status == "Pending" || o.Status == "InProgress" || o.Status == "Ready")
                    .OrderBy(o => o.OrderTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading pending orders: {ex.Message}");
            }
        }

        public static async Task<List<Order>> GetAllKitchenOrdersAsync(int days = 1)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                var startDate = DateTime.Now.AddDays(-days);

                return await context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Include(o => o.Table)
                    .Include(o => o.Waiter)
                        .ThenInclude(w => w.User)
                    .Include(o => o.Chef)
                        .ThenInclude(c => c.User)
                    .Where(o => o.OrderTime >= startDate)
                    .OrderByDescending(o => o.OrderTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading kitchen orders: {ex.Message}");
            }
        }

        public static async Task<bool> UpdateOrderStatusAsync(int orderId, string newStatus, int? chefId = null)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    var order = await context.Orders.FindAsync(orderId);
                    if (order == null)
                        throw new Exception("Order not found");

                    // Ensure chef exists if chefId is provided
                    if (chefId.HasValue)
                    {
                        await EnsureChefExistsAsync(chefId.Value);
                        order.ChefID = chefId.Value;
                    }

                    order.Status = newStatus;

                    // If order is marked as served, free up the table
                    if (newStatus == "Served")
                    {
                        var table = await context.Tables.FindAsync(order.TableID);
                        if (table != null)
                        {
                            table.Status = "Available";
                        }
                    }

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating order status: {ex.Message}");
            }
        }

        public static async Task<bool> AcceptOrderAsync(int orderId, int chefId)
        {
            return await UpdateOrderStatusAsync(orderId, "InProgress", chefId);
        }

        public static async Task<bool> MarkOrderReadyAsync(int orderId)
        {
            return await UpdateOrderStatusAsync(orderId, "Ready");
        }

        public static async Task<bool> MarkOrderServedAsync(int orderId)
        {
            return await UpdateOrderStatusAsync(orderId, "Served");
        }

        public static async Task<Dictionary<string, int>> GetOrderStatisticsAsync()
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                var today = DateTime.Today;

                var stats = new Dictionary<string, int>();

                stats["TodayTotal"] = await context.Orders
                    .Where(o => o.OrderTime >= today)
                    .CountAsync();

                stats["Pending"] = await context.Orders
                    .Where(o => o.Status == "Pending")
                    .CountAsync();

                stats["InProgress"] = await context.Orders
                    .Where(o => o.Status == "InProgress")
                    .CountAsync();

                stats["Ready"] = await context.Orders
                    .Where(o => o.Status == "Ready")
                    .CountAsync();

                stats["Served"] = await context.Orders
                    .Where(o => o.OrderTime >= today && o.Status == "Served")
                    .CountAsync();

                return stats;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading order statistics: {ex.Message}");
            }
        }
    }
}