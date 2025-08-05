using FoodOrderingSystem.Models;
// Forms/ChefDashboard.cs
using FoodOrderingSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodOrderingSystem.Extensions;
using FoodOrderingSystem.Constants;


namespace FoodOrderingSystem.Forms
{
    public partial class ChefDashboard : Form
    {
        private System.Windows.Forms.Timer refreshTimer;
        private List<Order> currentOrders = new List<Order>();

        public ChefDashboard()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"Chef Dashboard - {AuthenticationService.CurrentUser?.Name}";

            // Initialize refresh timer for real-time updates
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 10000; // Refresh every 10 seconds
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();

            LoadOrderStatistics();
            LoadPendingOrders();
            LoadAllOrders();
        }

        private async void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            await RefreshDataAsync();
        }

        private async Task RefreshDataAsync()
        {
            try
            {
                await LoadOrderStatistics();
                await LoadPendingOrders();
                if (tabControl.SelectedTab == tabAllOrders)
                {
                    await LoadAllOrders();
                }
            }
            catch (Exception ex)
            {
                // Silent refresh error - don't show message box during auto-refresh
                Console.WriteLine($"Auto-refresh error: {ex.Message}");
            }
        }

        private async Task LoadOrderStatistics()
        {
            try
            {
                var stats = await ChefService.GetOrderStatisticsAsync();

                lblTodayTotal.Text = $"Today's Orders: {stats["TodayTotal"]}";
                lblPendingCount.Text = $"Pending: {stats["Pending"]}";
                lblInProgressCount.Text = $"In Progress: {stats["InProgress"]}";
                lblReadyCount.Text = $"Ready: {stats["Ready"]}";
                lblServedCount.Text = $"Served: {stats["Served"]}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading statistics: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadPendingOrders()
        {
            try
            {
                var orders = await ChefService.GetPendingOrdersAsync();
                currentOrders = orders;

                lvPendingOrders.Items.Clear();

                foreach (var order in orders)
                {
                    var listItem = new ListViewItem($"#{order.OrderID}");
                    listItem.SubItems.Add($"Table {order.TableNumber}");
                    listItem.SubItems.Add(order.OrderTime.ToString("HH:mm"));
                    listItem.SubItems.Add(GetElapsedTime(order.OrderTime));
                    listItem.SubItems.Add(order.Status);
                    listItem.SubItems.Add(order.Waiter?.User?.Name ?? "Unknown");
                    listItem.SubItems.Add($"{order.OrderItems.Sum(oi => oi.Quantity)} items");
                    listItem.Tag = order;

                    // Color code by urgency
                    var elapsed = DateTime.Now - order.OrderTime;
                    if (elapsed.TotalMinutes > 20)
                        listItem.BackColor = Color.FromArgb(255, 220, 220); // Light red - urgent
                    else if (elapsed.TotalMinutes > 10)
                        listItem.BackColor = Color.FromArgb(255, 248, 220); // Light yellow - attention
                    else if (order.Status == "InProgress")
                        listItem.BackColor = Color.FromArgb(220, 248, 255); // Light blue - in progress

                    lvPendingOrders.Items.Add(listItem);
                }

                lblPendingInfo.Text = $"Showing {orders.Count} active orders - Auto-refresh every 10 seconds";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pending orders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadAllOrders()
        {
            try
            {
                var orders = await ChefService.GetAllKitchenOrdersAsync();

                lvAllOrders.Items.Clear();

                foreach (var order in orders)
                {
                    var listItem = new ListViewItem($"#{order.OrderID}");
                    listItem.SubItems.Add($"Table {order.TableNumber}");
                    listItem.SubItems.Add(order.OrderTime.ToString("MM/dd HH:mm"));
                    listItem.SubItems.Add(order.Status);
                    listItem.SubItems.Add(order.Waiter?.User?.Name ?? "Unknown");
                    listItem.SubItems.Add(order.Chef?.User?.Name ?? "Unassigned");
                    listItem.SubItems.Add($"${order.TotalPrice:F2}");
                    listItem.SubItems.Add($"{order.OrderItems.Sum(oi => oi.Quantity)} items");
                    listItem.Tag = order;

                    // Color code by status
                    switch (order.Status.ToLower())
                    {
                        case "pending":
                            listItem.BackColor = Color.FromArgb(255, 248, 220); // Light yellow
                            break;
                        case "inprogress":
                            listItem.BackColor = Color.FromArgb(220, 248, 255); // Light blue
                            break;
                        case "ready":
                            listItem.BackColor = Color.FromArgb(220, 255, 220); // Light green
                            break;
                        case "served":
                            listItem.BackColor = Color.FromArgb(248, 248, 248); // Light gray
                            break;
                    }

                    lvAllOrders.Items.Add(listItem);
                }

                lblAllOrdersInfo.Text = $"Showing {orders.Count} orders from today";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading all orders: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetElapsedTime(DateTime orderTime)
        {
            var elapsed = DateTime.Now - orderTime;
            if (elapsed.TotalMinutes < 1)
                return "Just now";
            else if (elapsed.TotalHours < 1)
                return $"{(int)elapsed.TotalMinutes}m ago";
            else
                return $"{(int)elapsed.TotalHours}h {elapsed.Minutes}m ago";
        }

        private async void btnAcceptOrder_Click(object sender, EventArgs e)
        {
            if (lvPendingOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order to accept.", "No Order Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedOrder = (Order)lvPendingOrders.SelectedItems[0].Tag;

            if (selectedOrder.Status != "Pending")
            {
                MessageBox.Show("Only pending orders can be accepted.", "Invalid Action",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnAcceptOrder.Enabled = false;
                btnAcceptOrder.Text = "Accepting...";

                bool success = await ChefService.AcceptOrderAsync(selectedOrder.OrderID,
                    AuthenticationService.CurrentUser!.UserID);

                if (success)
                {
                    MessageBox.Show($"Order #{selectedOrder.OrderID} accepted and moved to preparation!",
                        "Order Accepted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPendingOrders();
                    await LoadOrderStatistics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error accepting order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAcceptOrder.Enabled = true;
                btnAcceptOrder.Text = "Accept Order";
            }
        }

        private async void btnMarkReady_Click(object sender, EventArgs e)
        {
            if (lvPendingOrders.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order to mark as ready.", "No Order Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedOrder = (Order)lvPendingOrders.SelectedItems[0].Tag;

            if (selectedOrder.Status != "InProgress")
            {
                MessageBox.Show("Only orders in progress can be marked as ready.", "Invalid Action",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnMarkReady.Enabled = false;
                btnMarkReady.Text = "Updating...";

                bool success = await ChefService.MarkOrderReadyAsync(selectedOrder.OrderID);

                if (success)
                {
                    MessageBox.Show($"Order #{selectedOrder.OrderID} marked as ready for serving!",
                        "Order Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPendingOrders();
                    await LoadOrderStatistics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error marking order ready: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnMarkReady.Enabled = true;
                btnMarkReady.Text = "Mark Ready";
            }
        }

        private async void btnMarkServed_Click(object sender, EventArgs e)
        {
            ListView targetListView = tabControl.SelectedTab == tabPendingOrders ? lvPendingOrders : lvAllOrders;

            if (targetListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order to mark as served.", "No Order Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedOrder = (Order)targetListView.SelectedItems[0].Tag;

            if (selectedOrder.Status != "Ready")
            {
                MessageBox.Show("Only ready orders can be marked as served.", "Invalid Action",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnMarkServed.Enabled = false;
                btnMarkServed.Text = "Updating...";

                bool success = await ChefService.MarkOrderServedAsync(selectedOrder.OrderID);

                if (success)
                {
                    MessageBox.Show($"Order #{selectedOrder.OrderID} marked as served! Table is now available.",
                        "Order Served", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadPendingOrders();
                    await LoadAllOrders();
                    await LoadOrderStatistics();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error marking order served: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnMarkServed.Enabled = true;
                btnMarkServed.Text = "Mark Served";
            }
        }

        private void btnViewOrderDetails_Click(object sender, EventArgs e)
        {
            ListView targetListView = tabControl.SelectedTab == tabPendingOrders ? lvPendingOrders : lvAllOrders;

            if (targetListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an order to view details.", "No Order Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedOrder = (Order)targetListView.SelectedItems[0].Tag;
            ShowOrderDetails(selectedOrder);
        }

        private void ShowOrderDetails(Order order)
        {
            var details = new System.Text.StringBuilder();
            details.AppendLine($"Order #{order.OrderID} Details");
            details.AppendLine($"═══════════════════════════");
            details.AppendLine($"Table: {order.TableNumber}");
            details.AppendLine($"Order Time: {order.OrderTime:yyyy-MM-dd HH:mm:ss}");
            details.AppendLine($"Status: {order.Status}");
            details.AppendLine($"Waiter: {order.Waiter?.User?.Name ?? "Unknown"}");
            details.AppendLine($"Chef: {order.Chef?.User?.Name ?? "Not assigned"}");
            details.AppendLine($"Total: ${order.TotalPrice:F2}");
            details.AppendLine();
            details.AppendLine("Order Items:");
            details.AppendLine("─────────────────────────────");

            foreach (var item in order.OrderItems)
            {
                details.AppendLine($"• {item.Quantity}x {item.MenuItem.Name} - ${item.MenuItem.Price:F2} each");
                if (!string.IsNullOrEmpty(item.SpecialInstructions))
                {
                    details.AppendLine($"  Special: {item.SpecialInstructions}");
                }
            }

            MessageBox.Show(details.ToString(), "Order Details",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void btnRefreshOrders_Click(object sender, EventArgs e)
        {
            btnRefreshOrders.Enabled = false;
            btnRefreshOrders.Text = "Refreshing...";

            try
            {
                await RefreshDataAsync();
            }
            finally
            {
                btnRefreshOrders.Enabled = true;
                btnRefreshOrders.Text = "Refresh";
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                refreshTimer?.Stop();
                refreshTimer?.Dispose();
                AuthenticationService.Logout();
                this.Close();
            }
        }
    }
}
