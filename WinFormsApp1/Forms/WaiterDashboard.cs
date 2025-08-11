// Forms/WaiterDashboard.cs
using FoodOrderingSystem.Services;
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Forms
{
    public partial class WaiterDashboard : Form
    {
        private List<MenuItem> menuItems = new List<MenuItem>();
        private List<OrderItemRequest> currentOrder = new List<OrderItemRequest>();

        // Add image support
        private ImageList? menuImageList;

        public WaiterDashboard()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"Waiter Dashboard - {AuthenticationService.CurrentUser?.Name}";
            InitializeMenuImageSupport(); // Initialize image support
            LoadMenuItems();
            LoadTables();
            RefreshCurrentOrder();
            LoadOrderHistory();
        }

        private void InitializeMenuImageSupport()
        {
            // Initialize image list for menu items
            menuImageList = new ImageList();
            menuImageList.ImageSize = new Size(64, 64); // Size for ListView images
            menuImageList.ColorDepth = ColorDepth.Depth32Bit;

            // Set the image list to the ListView
            lvMenu.LargeImageList = menuImageList;
            lvMenu.SmallImageList = menuImageList;

            // Set ListView to show images with details
            lvMenu.View = View.Details;

            // Set a minimum row height to properly display 64x64 images
            SetListViewRowHeight();
        }

        private void SetListViewRowHeight()
        {
            // Set a minimum row height to properly display 64x64 images
            // This is done by creating a temporary ImageList with larger images
            var tempImageList = new ImageList();
            tempImageList.ImageSize = new Size(1, 70); // Height of 70px for better image visibility
            lvMenu.SmallImageList = tempImageList;
            lvMenu.SmallImageList = menuImageList; // Reset to actual images
        }

        private async void LoadMenuItems()
        {
            try
            {
                Console.WriteLine("Loading menu items...");
                menuItems = await MenuService.GetAllMenuItemsAsync();
                Console.WriteLine($"Loaded {menuItems.Count} menu items");

                await LoadMenuImages(); // Load images first
                DisplayMenuItems();
                await LoadCategories();

                Console.WriteLine("Menu loading completed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading menu: {ex}");
                MessageBox.Show($"Error loading menu: {ex.Message}\n\nPlease check the database connection and try refreshing.", "Menu Loading Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadMenuImages()
        {
            if (menuImageList == null) return;

            menuImageList.Images.Clear();

            foreach (var item in menuItems)
            {
                try
                {
                    Image? itemImage = null;

                    // Load image if path exists
                    if (!string.IsNullOrEmpty(item.ImagePath))
                    {
                        // Try to load from application directory
                        string imagePath = Path.Combine(Application.StartupPath, item.ImagePath);

                        if (File.Exists(imagePath))
                        {
                            itemImage = Image.FromFile(imagePath);
                        }
                        else if (File.Exists(item.ImagePath))
                        {
                            itemImage = Image.FromFile(item.ImagePath);
                        }
                    }

                    // Create placeholder if no image found
                    if (itemImage == null)
                    {
                        itemImage = CreatePlaceholderImage(item.Name);
                    }

                    // Resize image to fit the ListView
                    var resizedImage = ResizeImage(itemImage, 64, 64);
                    menuImageList.Images.Add(item.ItemID.ToString(), resizedImage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading image for {item.Name}: {ex.Message}");
                    // Add placeholder image
                    var placeholder = CreatePlaceholderImage(item.Name);
                    var resizedPlaceholder = ResizeImage(placeholder, 64, 64);
                    menuImageList.Images.Add(item.ItemID.ToString(), resizedPlaceholder);
                }
            }
        }

        private Image CreatePlaceholderImage(string itemName)
        {
            var placeholder = new Bitmap(64, 64);
            using (var g = Graphics.FromImage(placeholder))
            {
                g.Clear(Color.LightGray);
                g.DrawRectangle(Pens.Gray, 0, 0, 63, 63);

                // Draw text
                using (var brush = new SolidBrush(Color.DarkGray))
                using (var font = new Font("Segoe UI", 6F))
                {
                    var textRect = new Rectangle(2, 2, 60, 60);
                    var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString("No Image", font, brush, textRect, sf);
                }
            }
            return placeholder;
        }

        private Image ResizeImage(Image image, int width, int height)
        {
            var resized = new Bitmap(width, height);
            using (var g = Graphics.FromImage(resized))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, 0, 0, width, height);
            }
            return resized;
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
            if (menuImageList == null) return;

            lvMenu.Items.Clear();

            foreach (var item in menuItems)
            {
                var listItem = new ListViewItem(item.Name);
                listItem.SubItems.Add(item.Category);
                listItem.SubItems.Add($"LKR {item.Price:F2}"); // Changed from $ to LKR
                listItem.SubItems.Add(item.Description ?? "");

                // Set the image for this item
                if (menuImageList.Images.ContainsKey(item.ItemID.ToString()))
                {
                    listItem.ImageKey = item.ItemID.ToString();
                }

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
                await LoadMenuImages(); // Reload images for filtered items
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

            try
            {
                var selectedItem = (MenuItem)lvMenu.SelectedItems[0].Tag;
                var quantity = (int)nudQuantity.Value;
                var specialInstructions = txtSpecialInstructions.Text.Trim();

                Console.WriteLine($"Adding to order: {selectedItem.Name} x{quantity}");

                // Check if item already exists in current order
                var existingItem = currentOrder.FirstOrDefault(oi => oi.ItemID == selectedItem.ItemID &&
                                                                     oi.SpecialInstructions == specialInstructions);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                    Console.WriteLine($"Updated existing item quantity to {existingItem.Quantity}");
                }
                else
                {
                    currentOrder.Add(new OrderItemRequest
                    {
                        ItemID = selectedItem.ItemID,
                        Quantity = quantity,
                        SpecialInstructions = string.IsNullOrEmpty(specialInstructions) ? null : specialInstructions
                    });
                    Console.WriteLine($"Added new item to order. Total items in order: {currentOrder.Count}");
                }

                RefreshCurrentOrder();

                // Reset form
                nudQuantity.Value = 1;
                txtSpecialInstructions.Clear();

                MessageBox.Show($"Added {quantity}x {selectedItem.Name} to order!", "Item Added",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding item to order: {ex}");
                MessageBox.Show($"Error adding item to order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshCurrentOrder()
        {
            lvCurrentOrder.Items.Clear();
            decimal total = 0;

            foreach (var orderItem in currentOrder)
            {
                // Find the menu item - search in the loaded menuItems list first
                var menuItem = menuItems.FirstOrDefault(mi => mi.ItemID == orderItem.ItemID);

                // If not found in current list, we need to load it
                if (menuItem == null)
                {
                    // This should not happen if menu is loaded properly, but let's handle it
                    try
                    {
                        menuItem = MenuService.GetMenuItemByIdAsync(orderItem.ItemID).Result;
                    }
                    catch
                    {
                        // Skip this item if we can't load it
                        continue;
                    }
                }

                if (menuItem != null)
                {
                    var listItem = new ListViewItem(menuItem.Name);
                    listItem.SubItems.Add(orderItem.Quantity.ToString());
                    listItem.SubItems.Add($"LKR {menuItem.Price:F2}"); // Changed from $ to LKR
                    listItem.SubItems.Add($"LKR {(menuItem.Price * orderItem.Quantity):F2}"); // Changed from $ to LKR
                    listItem.SubItems.Add(orderItem.SpecialInstructions ?? "");
                    listItem.Tag = orderItem;
                    lvCurrentOrder.Items.Add(listItem);

                    total += menuItem.Price * orderItem.Quantity;
                }
            }

            lblOrderTotal.Text = $"Order Total: LKR {total:F2}"; // Changed from $ to LKR
            // Enable button when there are items, regardless of table selection
            btnSubmitOrder.Enabled = currentOrder.Count > 0;
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
            // Validation checks with detailed messages
            if (currentOrder.Count == 0)
            {
                MessageBox.Show("Please add items to the order first.", "Empty Order",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check table selection at submission time, not before
            if (cmbTable.SelectedIndex <= 0)
            {
                MessageBox.Show("Please select a table before submitting the order.", "No Table Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTable.Focus(); // Focus on the table combo box
                return;
            }

            if (AuthenticationService.CurrentUser == null)
            {
                MessageBox.Show("User session expired. Please login again.", "Session Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Show loading state
                btnSubmitOrder.Enabled = false;
                btnSubmitOrder.Text = "Submitting...";

                // Extract table ID from combo box text
                string tableText = cmbTable.SelectedItem.ToString()!;

                // Debug: Show what we're parsing
                Console.WriteLine($"Parsing table text: {tableText}");

                // Parse table ID more carefully
                int tableId;
                try
                {
                    // Extract number from "Table X (Capacity: Y)" format
                    var parts = tableText.Split(' ');
                    if (parts.Length < 2 || !int.TryParse(parts[1], out tableId))
                    {
                        throw new Exception($"Could not parse table ID from: {tableText}");
                    }
                }
                catch (Exception parseEx)
                {
                    MessageBox.Show($"Error parsing selected table: {parseEx.Message}", "Parsing Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Get waiter ID
                int waiterId = AuthenticationService.CurrentUser.UserID;

                // Debug information
                Console.WriteLine($"Submitting order: TableID={tableId}, WaiterID={waiterId}, Items={currentOrder.Count}");

                // Create the order
                int orderId = await OrderService.CreateOrderAsync(tableId, waiterId, currentOrder);

                // Success!
                MessageBox.Show($"Order #{orderId} submitted successfully!\n" +
                               $"Table: {tableId}\n" +
                               $"Items: {currentOrder.Count}\n" +
                               $"Total: {lblOrderTotal.Text}\n\n" +
                               $"Order sent to kitchen for preparation.",
                    "Order Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear the order and refresh
                currentOrder.Clear();
                RefreshCurrentOrder();
                LoadTables(); // Refresh available tables
                LoadOrderHistory(); // Refresh order history

                // Reset table selection
                cmbTable.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                // Show detailed error information
                var errorMessage = $"Error submitting order: {ex.Message}\n\n";
                errorMessage += $"Details:\n";
                errorMessage += $"- Current User: {AuthenticationService.CurrentUser?.Name ?? "None"}\n";
                errorMessage += $"- Selected Table: {cmbTable.SelectedItem?.ToString() ?? "None"}\n";
                errorMessage += $"- Order Items: {currentOrder.Count}\n";

                if (ex.InnerException != null)
                {
                    errorMessage += $"- Inner Exception: {ex.InnerException.Message}\n";
                }

                MessageBox.Show(errorMessage, "Submit Order Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Log to console for debugging
                Console.WriteLine($"Submit Order Error: {ex}");
            }
            finally
            {
                // Reset button state - Enable if there are still items
                btnSubmitOrder.Enabled = currentOrder.Count > 0;
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
                    listItem.SubItems.Add($"LKR {order.TotalPrice:F2}"); // Changed from $ to LKR
                    listItem.SubItems.Add($"{order.OrderItems.Sum(oi => oi.Quantity)} items");

                    // Add order details
                    var details = string.Join(", ", order.OrderItems.Select(oi => $"{oi.Quantity}x {oi.MenuItem.Name}"));
                    listItem.SubItems.Add(details);
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