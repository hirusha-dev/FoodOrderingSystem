using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Constants/OrderStatus.cs
namespace FoodOrderingSystem.Constants
{
    public static class OrderStatus
    {
        public const string Pending = "Pending";
        public const string InProgress = "InProgress";
        public const string Ready = "Ready";
        public const string Served = "Served";

        public static readonly string[] AllStatuses = { Pending, InProgress, Ready, Served };

        public static string GetDisplayName(string status)
        {
            return status switch
            {
                Pending => "Pending",
                InProgress => "In Progress",
                Ready => "Ready",
                Served => "Served",
                _ => status
            };
        }

        public static Color GetColor(string status)
        {
            return status switch
            {
                Pending => Color.FromArgb(255, 193, 7), // Yellow
                InProgress => Color.FromArgb(51, 122, 183), // Blue
                Ready => Color.FromArgb(40, 167, 69), // Green
                Served => Color.FromArgb(108, 117, 125), // Gray
                _ => Color.Black
            };
        }
    }
}