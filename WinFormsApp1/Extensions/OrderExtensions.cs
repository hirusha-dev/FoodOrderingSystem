using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Extensions/OrderExtensions.cs
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Extensions
{
    public static class OrderExtensions
    {
        public static Color GetStatusColor(this Order order)
        {
            return order.Status.ToLower() switch
            {
                "pending" => Color.FromArgb(255, 248, 220), // Light yellow
                "inprogress" => Color.FromArgb(220, 248, 255), // Light blue
                "ready" => Color.FromArgb(220, 255, 220), // Light green
                "served" => Color.FromArgb(248, 248, 248), // Light gray
                _ => Color.White
            };
        }

        public static Color GetUrgencyColor(this Order order)
        {
            var elapsed = DateTime.Now - order.OrderTime;

            if (elapsed.TotalMinutes > 20)
                return Color.FromArgb(255, 220, 220); // Light red - urgent
            else if (elapsed.TotalMinutes > 10)
                return Color.FromArgb(255, 248, 220); // Light yellow - attention needed
            else if (order.Status == "InProgress")
                return Color.FromArgb(220, 248, 255); // Light blue - in progress
            else
                return Color.White;
        }

        public static string GetElapsedTime(this Order order)
        {
            var elapsed = DateTime.Now - order.OrderTime;

            if (elapsed.TotalMinutes < 1)
                return "Just now";
            else if (elapsed.TotalHours < 1)
                return $"{(int)elapsed.TotalMinutes}m ago";
            else
                return $"{(int)elapsed.TotalHours}h {elapsed.Minutes}m ago";
        }

        public static bool CanAccept(this Order order)
        {
            return order.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase);
        }

        public static bool CanMarkReady(this Order order)
        {
            return order.Status.Equals("InProgress", StringComparison.OrdinalIgnoreCase);
        }

        public static bool CanMarkServed(this Order order)
        {
            return order.Status.Equals("Ready", StringComparison.OrdinalIgnoreCase);
        }

        public static string GetOrderSummary(this Order order)
        {
            var items = order.OrderItems
                .GroupBy(oi => oi.MenuItem.Name)
                .Select(g => $"{g.Sum(oi => oi.Quantity)}x {g.Key}")
                .ToList();

            return string.Join(", ", items);
        }

        public static int GetTotalItems(this Order order)
        {
            return order.OrderItems.Sum(oi => oi.Quantity);
        }
    }
}