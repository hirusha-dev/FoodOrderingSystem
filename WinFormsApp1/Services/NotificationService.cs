// Services/NotificationService.cs - Final Complete Version
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Services
{
    public static class NotificationService
    {
        // Event handlers for real-time notifications
        public static event EventHandler<OrderNotificationEventArgs>? OrderStatusChanged;
        public static event EventHandler<OrderNotificationEventArgs>? NewOrderReceived;
        public static event EventHandler<OrderNotificationEventArgs>? OrderReady;
        public static event EventHandler<OrderNotificationEventArgs>? OrderServed;
        public static event EventHandler<SystemNotificationEventArgs>? SystemAlert;

        // Notification settings
        public static bool NotificationsEnabled { get; set; } = true;
        public static bool SoundEnabled { get; set; } = true;
        public static bool SystemTrayEnabled { get; set; } = true;

        // Notification methods
        public static void NotifyNewOrder(Order order)
        {
            if (!NotificationsEnabled) return;

            var args = new OrderNotificationEventArgs
            {
                Order = order,
                Message = $"New order #{order.OrderID} from Table {order.TableNumber}",
                NotificationType = NotificationType.NewOrder,
                Timestamp = DateTime.Now
            };

            // Trigger event
            NewOrderReceived?.Invoke(null, args);

            // Play sound notification
            if (SoundEnabled)
                SoundService.PlayNewOrderSound();

            // Show system tray notification if available
            if (SystemTrayEnabled)
                ShowSystemNotification("New Order", args.Message, ToolTipIcon.Info);

            // Log notification for debugging
            LogNotification("NEW_ORDER", args.Message);
        }

        public static void NotifyOrderStatusChanged(Order order, string oldStatus, string newStatus)
        {
            if (!NotificationsEnabled) return;

            var args = new OrderNotificationEventArgs
            {
                Order = order,
                Message = $"Order #{order.OrderID} status changed: {oldStatus} → {newStatus}",
                NotificationType = NotificationType.StatusChanged,
                Timestamp = DateTime.Now,
                OldStatus = oldStatus,
                NewStatus = newStatus
            };

            // Trigger event
            OrderStatusChanged?.Invoke(null, args);

            // Specific notifications based on status
            if (newStatus.Equals("Ready", StringComparison.OrdinalIgnoreCase))
            {
                NotifyOrderReady(order);
            }
            else if (newStatus.Equals("Served", StringComparison.OrdinalIgnoreCase))
            {
                NotifyOrderServed(order);
            }

            // Log notification
            LogNotification("STATUS_CHANGED", args.Message);
        }

        public static void NotifyOrderReady(Order order)
        {
            if (!NotificationsEnabled) return;

            var args = new OrderNotificationEventArgs
            {
                Order = order,
                Message = $"Order #{order.OrderID} is ready for Table {order.TableNumber}!",
                NotificationType = NotificationType.OrderReady,
                Timestamp = DateTime.Now
            };

            // Trigger event
            OrderReady?.Invoke(null, args);

            // Play sound notification
            if (SoundEnabled)
                SoundService.PlayOrderReadySound();

            // Show system tray notification
            if (SystemTrayEnabled)
                ShowSystemNotification("Order Ready", args.Message, ToolTipIcon.Info);

            // Log notification
            LogNotification("ORDER_READY", args.Message);
        }

        public static void NotifyOrderServed(Order order)
        {
            if (!NotificationsEnabled) return;

            var args = new OrderNotificationEventArgs
            {
                Order = order,
                Message = $"Order #{order.OrderID} has been served. Table {order.TableNumber} is now available.",
                NotificationType = NotificationType.OrderServed,
                Timestamp = DateTime.Now
            };

            // Trigger event
            OrderServed?.Invoke(null, args);

            // Play sound notification
            if (SoundEnabled)
                SoundService.PlayOrderServedSound();

            // Log notification
            LogNotification("ORDER_SERVED", args.Message);
        }

        public static void NotifySystemAlert(string title, string message, SystemAlertLevel level = SystemAlertLevel.Info)
        {
            if (!NotificationsEnabled) return;

            var args = new SystemNotificationEventArgs
            {
                Title = title,
                Message = message,
                Level = level,
                Timestamp = DateTime.Now
            };

            // Trigger event
            SystemAlert?.Invoke(null, args);

            // Play appropriate sound
            if (SoundEnabled)
            {
                switch (level)
                {
                    case SystemAlertLevel.Success:
                        SoundService.PlaySuccessSound();
                        break;
                    case SystemAlertLevel.Warning:
                        SoundService.PlayErrorSound();
                        break;
                    case SystemAlertLevel.Error:
                        SoundService.PlayErrorSound();
                        break;
                    default:
                        SoundService.PlaySuccessSound();
                        break;
                }
            }

            // Show system tray notification
            if (SystemTrayEnabled)
            {
                var icon = level switch
                {
                    SystemAlertLevel.Error => ToolTipIcon.Error,
                    SystemAlertLevel.Warning => ToolTipIcon.Warning,
                    SystemAlertLevel.Success => ToolTipIcon.Info,
                    _ => ToolTipIcon.Info
                };
                ShowSystemNotification(title, message, icon);
            }

            // Log notification
            LogNotification($"SYSTEM_{level.ToString().ToUpper()}", $"{title}: {message}");
        }

        private static void ShowSystemNotification(string title, string message, ToolTipIcon icon)
        {
            try
            {
                // Create a temporary NotifyIcon for system notifications
                Task.Run(() =>
                {
                    using var notifyIcon = new NotifyIcon();
                    notifyIcon.Icon = SystemIcons.Information;
                    notifyIcon.Visible = true;
                    notifyIcon.BalloonTipTitle = title;
                    notifyIcon.BalloonTipText = message;
                    notifyIcon.BalloonTipIcon = icon;

                    notifyIcon.ShowBalloonTip(3000);

                    // Hide after showing
                    Thread.Sleep(3500);
                    notifyIcon.Visible = false;
                });
            }
            catch (Exception ex)
            {
                // Ignore notification errors - not critical
                Console.WriteLine($"System notification error: {ex.Message}");
            }
        }

        private static void LogNotification(string type, string message)
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine($"[{timestamp}] NOTIFICATION [{type}]: {message}");
            }
            catch
            {
                // Ignore logging errors
            }
        }

        // Utility methods
        public static void EnableNotifications()
        {
            NotificationsEnabled = true;
        }

        public static void DisableNotifications()
        {
            NotificationsEnabled = false;
        }

        public static void EnableSound()
        {
            SoundEnabled = true;
        }

        public static void DisableSound()
        {
            SoundEnabled = false;
        }

        public static void EnableSystemTray()
        {
            SystemTrayEnabled = true;
        }

        public static void DisableSystemTray()
        {
            SystemTrayEnabled = false;
        }

        // Clear all event handlers (useful for logout)
        public static void ClearAllNotifications()
        {
            OrderStatusChanged = null;
            NewOrderReceived = null;
            OrderReady = null;
            OrderServed = null;
            SystemAlert = null;

            LogNotification("SYSTEM", "All notification handlers cleared");
        }

        // Get notification statistics
        public static NotificationStats GetNotificationStats()
        {
            return new NotificationStats
            {
                NotificationsEnabled = NotificationsEnabled,
                SoundEnabled = SoundEnabled,
                SystemTrayEnabled = SystemTrayEnabled,
                ActiveSubscribers = new Dictionary<string, int>
                {
                    ["OrderStatusChanged"] = OrderStatusChanged?.GetInvocationList().Length ?? 0,
                    ["NewOrderReceived"] = NewOrderReceived?.GetInvocationList().Length ?? 0,
                    ["OrderReady"] = OrderReady?.GetInvocationList().Length ?? 0,
                    ["OrderServed"] = OrderServed?.GetInvocationList().Length ?? 0,
                    ["SystemAlert"] = SystemAlert?.GetInvocationList().Length ?? 0
                }
            };
        }
    }

    // Notification event arguments
    public class OrderNotificationEventArgs : EventArgs
    {
        public Order Order { get; set; } = null!;
        public string Message { get; set; } = string.Empty;
        public NotificationType NotificationType { get; set; }
        public DateTime Timestamp { get; set; }
        public string? OldStatus { get; set; }
        public string? NewStatus { get; set; }
    }

    // System notification event arguments
    public class SystemNotificationEventArgs : EventArgs
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public SystemAlertLevel Level { get; set; }
        public DateTime Timestamp { get; set; }
    }

    // Notification types
    public enum NotificationType
    {
        NewOrder,
        StatusChanged,
        OrderReady,
        OrderServed,
        SystemAlert
    }

    // System alert levels
    public enum SystemAlertLevel
    {
        Info,
        Success,
        Warning,
        Error
    }

    // Notification statistics
    public class NotificationStats
    {
        public bool NotificationsEnabled { get; set; }
        public bool SoundEnabled { get; set; }
        public bool SystemTrayEnabled { get; set; }
        public Dictionary<string, int> ActiveSubscribers { get; set; } = new();
    }
}