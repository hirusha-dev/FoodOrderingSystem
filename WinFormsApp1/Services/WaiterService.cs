using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Services/WaiterService.cs
using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Services
{
    public static class WaiterService
    {
        public static async Task<Waiter?> GetWaiterByUserIdAsync(int userId)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.Waiters
                    .Include(w => w.User)
                    .FirstOrDefaultAsync(w => w.UserID == userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading waiter: {ex.Message}");
            }
        }

        public static async Task<bool> EnsureWaiterExistsAsync(int userId)
        {
            try
            {
                using var context = DatabaseService.CreateContext();

                var waiter = await context.Waiters.FindAsync(userId);
                if (waiter == null)
                {
                    // Create waiter record if it doesn't exist
                    waiter = new Waiter
                    {
                        UserID = userId,
                        Specialty = "General Service",
                        ShiftTime = "Current Shift"
                    };

                    context.Waiters.Add(waiter);
                    await context.SaveChangesAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ensuring waiter exists: {ex.Message}");
            }
        }

        public static async Task<List<Order>> GetWaiterOrdersAsync(int waiterId, int days = 7)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                var startDate = DateTime.Now.AddDays(-days);

                return await context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Include(o => o.Table)
                    .Where(o => o.WaiterID == waiterId && o.OrderTime >= startDate)
                    .OrderByDescending(o => o.OrderTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading waiter orders: {ex.Message}");
            }
        }
    }
}