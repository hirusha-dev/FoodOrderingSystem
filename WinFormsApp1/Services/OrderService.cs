using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Services/OrderService.cs
using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Services
{
    public static class OrderService
    {
        public static async Task<List<Table>> GetAvailableTablesAsync()
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.Tables
                    .Where(table => table.Status == "Available")
                    .OrderBy(table => table.TableID)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading tables: {ex.Message}");
            }
        }

        public static async Task<int> CreateOrderAsync(int tableId, int waiterId, List<OrderItemRequest> orderItems)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                using var transaction = await context.Database.BeginTransactionAsync();

                try
                {
                    // Ensure waiter exists
                    await WaiterService.EnsureWaiterExistsAsync(waiterId);

                    // Create the order
                    var order = new Order
                    {
                        TableNumber = tableId,
                        TableID = tableId,
                        WaiterID = waiterId,
                        OrderTime = DateTime.Now,
                        Status = "Pending",
                        TotalPrice = 0
                    };

                    context.Orders.Add(order);
                    await context.SaveChangesAsync();

                    // Add order items and calculate total
                    decimal totalPrice = 0;

                    foreach (var itemRequest in orderItems)
                    {
                        var menuItem = await context.MenuItems.FindAsync(itemRequest.ItemID);
                        if (menuItem == null)
                            throw new Exception($"Menu item with ID {itemRequest.ItemID} not found");

                        var orderItem = new OrderItem
                        {
                            OrderID = order.OrderID,
                            ItemID = itemRequest.ItemID,
                            Quantity = itemRequest.Quantity,
                            SpecialInstructions = itemRequest.SpecialInstructions
                        };

                        context.OrderItems.Add(orderItem);
                        totalPrice += menuItem.Price * itemRequest.Quantity;
                    }

                    // Update order total
                    order.TotalPrice = totalPrice;

                    // Update table status
                    var table = await context.Tables.FindAsync(tableId);
                    if (table != null)
                    {
                        table.Status = "Occupied";
                    }

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return order.OrderID;
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating order: {ex.Message}");
            }
        }

        public static async Task<List<Order>> GetOrdersByWaiterAsync(int waiterId)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.Orders
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .Include(o => o.Table)
                    .Where(o => o.WaiterID == waiterId)
                    .OrderByDescending(o => o.OrderTime)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading orders: {ex.Message}");
            }
        }

        public static async Task<Order?> GetOrderByIdAsync(int orderId)
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
                    .FirstOrDefaultAsync(o => o.OrderID == orderId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading order: {ex.Message}");
            }
        }
    }

    public class OrderItemRequest
    {
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public string? SpecialInstructions { get; set; }
    }
}
