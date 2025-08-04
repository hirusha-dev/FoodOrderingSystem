using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Forms/WaiterDashboard.cs
using FoodOrderingSystem.Services;
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Forms
{
    public partial class WaiterDashboard : Form
    {
        private List<MenuItem> menuItems = new List<MenuItem>();
        private List<OrderItemRequest> currentOrder = new List<OrderItemRequest>();

        public WaiterDashboard()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"Waiter Dashboard - {AuthenticationService.CurrentUser?.Name}";
            LoadMenuItems();
            LoadTables();
            RefreshCurrentOrder();
            LoadOrderHistory();
        }

        private async void LoadMenuItems()
        {
            try
            {
                menuItems = await MenuService.GetAllMenuItemsAsync();
                DisplayMenuItems();
                await LoadCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading menu: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCategories()
        {
            try
            {
                var categories = await MenuService.GetAllCategoriesAsync();
                cmbCategory.Items.Clear();
                cmbCategory.Items.Add("All Categories");
                cmbCategory.Items.AddRange(categories.ToArray());
                cmbCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadTables()
        {
            try
            {
                var tables = await OrderService.GetAvailableTablesAsync();
                cmbTable.Items.Clear();
                cmbTable.Items.Add("Select Table");

                foreach (var table in tables)
                {
                    cmbTable.Items.Add($"Table {table.TableID} (Capacity: {table.Capacity})");
                }
                cmbTable.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tables: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayMenuItems()
        {
            lvMenu.Items.Clear();

            foreach (var item in menuItems)
            {
                var listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Category);
                listItem.SubItems.Add($"${item.Price:F2}");
                listItem.SubItems.Add(item.Description ?? "");
                listItem.Tag = item;
                lvMenu.Items.Add(listItem);
            }
        }

        private async void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategory.SelectedIndex == 0) // "All Categories"
                {
                    menuItems = await MenuService.GetAllMenuItemsAsync();
                }
                else
                {
                    string selectedCategory = cmbCategory.SelectedItem.ToString()!;
                    menuItems = await MenuService.GetMenuItemsByCategoryAsync(selectedCategory);
                }
                DisplayMenuItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering menu: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddToOrder_Click(object sender, EventArgs e)
        {
            if (lvMenu.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a menu item first.", "No Item Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudQuantity.Value < 1)
            {
                MessageBox.Show("Quantity must be at least 1.", "Invalid Quantity",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedItem = (MenuItem)lvMenu.SelectedItems[0].Tag;
            var quantity = (int)nudQuantity.Value;
            var specialInstructions = txtSpecialInstructions.Text.Trim();

            // Check if item already exists in current order
            var existingItem = currentOrder.FirstOrDefault(oi => oi.ItemID == selectedItem.ItemID &&
                                                                 oi.SpecialInstructions == specialInstructions);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                currentOrder.Add(new OrderItemRequest
                {
                    ItemID = selectedItem.ItemID,
                    Quantity = quantity,
                    SpecialInstructions = string.IsNullOrEmpty(specialInstructions) ? null : specialInstructions
                });
            }

            RefreshCurrentOrder();

            // Reset form
            nudQuantity.Value = 1;
            txtSpecialInstructions.Clear();

            MessageBox.Show($"Added {quantity}x {selectedItem.Name} to order!", "Item Added",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RefreshCurrentOrder()
        {
            lvCurrentOrder.Items.Clear();
            decimal total = 0;

            foreach (var orderItem in currentOrder)
            {
                var menuItem = menuItems.FirstOrDefault(mi => mi.ItemID == orderItem.ItemID);
                if (menuItem != null)
                {
                    var listItem = new ListViewItem(menuItem.Name);
                    listItem.SubItems.Add(orderItem.Quantity.ToString());
                    listItem.SubItems.Add($"${menuItem.Price:F2}");
                    listItem.SubItems.Add($"${(menuItem.Price * orderItem.Quantity):F2}");
                    listItem.SubItems.Add(orderItem.SpecialInstructions ?? "");
                    listItem.Tag = orderItem;
                    lvCurrentOrder.Items.Add(listItem);

                    total += menuItem.Price * orderItem.Quantity;
                }
            }

            lblOrderTotal.Text = $"Order Total: ${total:F2}";
            btnSubmitOrder.Enabled = currentOrder.Count > 0 && cmbTable.SelectedIndex > 0;
        }

        private void btnRemoveFromOrder_Click(object sender, EventArgs e)
        {
            if (lvCurrentOrder.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select an item to remove.", "No Item Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedOrderItem = (OrderItemRequest)lvCurrentOrder.SelectedItems[0].Tag;
            currentOrder.Remove(selectedOrderItem);
            RefreshCurrentOrder();
        }

        private void btnClearOrder_Click(object sender, EventArgs e)
        {
            if (currentOrder.Count == 0) return;

            var result = MessageBox.Show("Are you sure you want to clear the entire order?",
                "Clear Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                currentOrder.Clear();
                RefreshCurrentOrder();
            }
        }

        private async void btnSubmitOrder_Click(object sender, EventArgs e)
        {
            if (currentOrder.Count == 0)
            {
                MessageBox.Show("Please add items to the order first.", "Empty Order",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbTable.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a table.", "No Table Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSubmitOrder.Enabled = false;
                btnSubmitOrder.Text = "Submitting...";

                // Extract table ID from combo box text
                string tableText = cmbTable.SelectedItem.ToString()!;
                int tableId = int.Parse(tableText.Split(' ')[1]);

                // Get waiter ID (assuming we have a way to get this)
                int waiterId = AuthenticationService.CurrentUser!.UserID;

                int orderId = await OrderService.CreateOrderAsync(tableId, waiterId, currentOrder);

                MessageBox.Show($"Order #{orderId} submitted successfully!\nSent to kitchen for preparation.",
                    "Order Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the order and refresh
                currentOrder.Clear();
                RefreshCurrentOrder();
                LoadTables(); // Refresh available tables
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error submitting order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubmitOrder.Enabled = true;
                btnSubmitOrder.Text = "Submit Order";
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AuthenticationService.Logout();
                this.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMenuItems();
            LoadTables();
        }

        private async void LoadOrderHistory()
        {
            try
            {
                if (AuthenticationService.CurrentUser == null) return;

                var orders = await WaiterService.GetWaiterOrdersAsync(AuthenticationService.CurrentUser.UserID);

                lvOrderHistory.Items.Clear();

                foreach (var order in orders)
                {
                    var listItem = new ListViewItem($"#{order.OrderID}");
                    listItem.SubItems.Add($"Table {order.TableNumber}");
                    listItem.SubItems.Add(order.OrderTime.ToString("MM/dd/yyyy HH:mm"));
                    listItem.SubItems.Add(order.Status);
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

                    lvOrderHistory.Items.Add(listItem);
                }

                lblHistoryInfo.Text = $"Showing {orders.Count} orders from the last 7 days";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order history: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}