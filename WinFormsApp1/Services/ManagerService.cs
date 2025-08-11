// Services/ManagerService.cs
using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Services
{
    public static class ManagerService
    {
        // User Management
        public static async Task<bool> CreateUserAsync(string name, string username, string password, string role)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    // Check if username already exists
                    var existingUser = await context.Users
                        .FirstOrDefaultAsync(u => u.Username == username);

                    if (existingUser != null)
                        throw new Exception("Username already exists");

                    // Create user
                    var user = new User
                    {
                        Name = name,
                        Username = username,
                        Password = password, // In production, this should be hashed
                        Role = role,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    };

                    context.Users.Add(user);
                    await context.SaveChangesAsync();

                    // Create role-specific record
                    if (role == "Waiter")
                    {
                        var waiter = new Waiter
                        {
                            UserID = user.UserID,
                            Specialty = "General Service",
                            ShiftTime = "Day Shift"
                        };
                        context.Waiters.Add(waiter);
                    }
                    else if (role == "Chef")
                    {
                        var chef = new Chef
                        {
                            UserID = user.UserID,
                            Specialty = "General Cooking",
                            ShiftTime = "Day Shift"
                        };
                        context.Chefs.Add(chef);
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
                throw new Exception($"Error creating user: {ex.Message}");
            }
        }

        public static async Task<bool> UpdateUserAsync(int userId, string name, string username, string role, bool isActive)
        {
            try
            {
                using var context = DatabaseService.CreateContext();

                var user = await context.Users.FindAsync(userId);
                if (user == null)
                    throw new Exception("User not found");

                user.Name = name;
                user.Username = username;
                user.Role = role;
                user.IsActive = isActive;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user: {ex.Message}");
            }
        }

        public static async Task<bool> DeleteUserAsync(int userId)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    var user = await context.Users.FindAsync(userId);
                    if (user == null)
                        throw new Exception("User not found");

                    // Check if user has orders
                    var hasOrders = await context.Orders.AnyAsync(o => o.WaiterID == userId || o.ChefID == userId);
                    if (hasOrders)
                    {
                        // Don't delete, just deactivate
                        user.IsActive = false;
                    }
                    else
                    {
                        // Remove role-specific records first
                        var waiter = await context.Waiters.FindAsync(userId);
                        if (waiter != null) context.Waiters.Remove(waiter);

                        var chef = await context.Chefs.FindAsync(userId);
                        if (chef != null) context.Chefs.Remove(chef);

                        // Remove user
                        context.Users.Remove(user);
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
                throw new Exception($"Error deleting user: {ex.Message}");
            }
        }

        public static async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.Users
                    .OrderBy(u => u.Role)
                    .ThenBy(u => u.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading users: {ex.Message}");
            }
        }

        // Table Management
        public static async Task<bool> CreateTableAsync(int capacity)
        {
            try
            {
                using var context = DatabaseService.CreateContext();

                var table = new Table
                {
                    Capacity = capacity,
                    Status = "Available"
                };

                context.Tables.Add(table);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating table: {ex.Message}");
            }
        }

        public static async Task<bool> UpdateTableAsync(int tableId, int capacity, string status)
        {
            try
            {
                using var context = DatabaseService.CreateContext();

                var table = await context.Tables.FindAsync(tableId);
                if (table == null)
                    throw new Exception("Table not found");

                table.Capacity = capacity;
                table.Status = status;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating table: {ex.Message}");
            }
        }

        public static async Task<bool> DeleteTableAsync(int tableId)
        {
            try
            {
                using var context = DatabaseService.CreateContext();

                // Check if table has orders
                var hasOrders = await context.Orders.AnyAsync(o => o.TableID == tableId);
                if (hasOrders)
                    throw new Exception("Cannot delete table with existing orders");

                var table = await context.Tables.FindAsync(tableId);
                if (table == null)
                    throw new Exception("Table not found");

                context.Tables.Remove(table);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting table: {ex.Message}");
            }
        }

        public static async Task<List<Table>> GetAllTablesAsync()
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.Tables
                    .OrderBy(t => t.TableID)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading tables: {ex.Message}");
            }
        }

        // Reporting
        public static async Task<Dictionary<string, object>> GetSalesReportAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                using var context = DatabaseService.CreateContext();

                var orders = await context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Where(o => o.OrderTime >= startDate && o.OrderTime <= endDate && o.Status == "Served")
                    .ToListAsync();

                var report = new Dictionary<string, object>
                {
                    ["TotalOrders"] = orders.Count,
                    ["TotalRevenue"] = orders.Sum(o => o.TotalPrice),
                    ["AverageOrderValue"] = orders.Count > 0 ? orders.Average(o => o.TotalPrice) : 0,
                    ["TotalItems"] = orders.SelectMany(o => o.OrderItems).Sum(oi => oi.Quantity),
                    ["TopSellingItems"] = orders
                        .SelectMany(o => o.OrderItems)
                        .GroupBy(oi => oi.MenuItem.Name)
                        .Select(g => new { Item = g.Key, Quantity = g.Sum(oi => oi.Quantity) })
                        .OrderByDescending(x => x.Quantity)
                        .Take(5)
                        .ToList(),
                    ["DailyRevenue"] = orders
                        .GroupBy(o => o.OrderTime.Date)
                        .Select(g => new { Date = g.Key, Revenue = g.Sum(o => o.TotalPrice) })
                        .OrderBy(x => x.Date)
                        .ToList()
                };

                return report;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating sales report: {ex.Message}");
            }
        }

        public static async Task<Dictionary<string, object>> GetPerformanceReportAsync()
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                var today = DateTime.Today;
                var thisWeek = today.AddDays(-7);

                var todayOrders = await context.Orders
                    .Where(o => o.OrderTime >= today)
                    .ToListAsync();

                var weekOrders = await context.Orders
                    .Where(o => o.OrderTime >= thisWeek)
                    .ToListAsync();

                // Calculate average preparation time
                var completedOrders = await context.Orders
                    .Where(o => o.Status == "Served" && o.OrderTime >= thisWeek)
                    .ToListAsync();

                var avgPrepTime = completedOrders.Count > 0
                    ? completedOrders.Average(o => (DateTime.Now - o.OrderTime).TotalMinutes)
                    : 0;

                var report = new Dictionary<string, object>
                {
                    ["TodayOrders"] = todayOrders.Count,
                    ["TodayRevenue"] = todayOrders.Where(o => o.Status == "Served").Sum(o => o.TotalPrice),
                    ["WeekOrders"] = weekOrders.Count,
                    ["WeekRevenue"] = weekOrders.Where(o => o.Status == "Served").Sum(o => o.TotalPrice),
                    ["AveragePreparationTime"] = Math.Round(avgPrepTime, 1),
                    ["PendingOrders"] = await context.Orders.CountAsync(o => o.Status == "Pending"),
                    ["InProgressOrders"] = await context.Orders.CountAsync(o => o.Status == "InProgress"),
                    ["ReadyOrders"] = await context.Orders.CountAsync(o => o.Status == "Ready"),
                    ["ActiveWaiters"] = await context.Waiters.CountAsync(),
                    ["ActiveChefs"] = await context.Chefs.CountAsync(),
                    ["AvailableTables"] = await context.Tables.CountAsync(t => t.Status == "Available"),
                    ["OccupiedTables"] = await context.Tables.CountAsync(t => t.Status == "Occupied")
                };

                return report;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating performance report: {ex.Message}");
            }
        }
    }
}