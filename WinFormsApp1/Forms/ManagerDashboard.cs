// Forms/ManagerDashboard.cs
using FoodOrderingSystem.Services;
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Forms
{
    public partial class ManagerDashboard : Form
    {
        private System.Windows.Forms.Timer refreshTimer;
        private List<User> currentUsers = new List<User>();
        private List<Table> currentTables = new List<Table>();

        public ManagerDashboard()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"Manager Dashboard - {AuthenticationService.CurrentUser?.Name}";

            // Initialize refresh timer
            refreshTimer = new System.Windows.Forms.Timer();
            refreshTimer.Interval = 10000; // Refresh every 30 seconds
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();

            LoadPerformanceData();
            LoadUsersData();
            LoadTablesData();
            LoadSalesReport();
        }

        private async void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            try
            {
                await LoadPerformanceData();
                if (tabControl.SelectedTab == tabUsers)
                    await LoadUsersData();
                else if (tabControl.SelectedTab == tabTables)
                    await LoadTablesData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Auto-refresh error: {ex.Message}");
            }
        }

        private async Task LoadPerformanceData()
        {
            try
            {
                var report = await ManagerService.GetPerformanceReportAsync();

                // Update performance labels
                lblTodayOrders.Text = $"Today's Orders: {report["TodayOrders"]}";
                lblTodayRevenue.Text = $"Today's Revenue: ${report["TodayRevenue"]:F2}";
                lblWeekOrders.Text = $"This Week: {report["WeekOrders"]} orders";
                lblWeekRevenue.Text = $"Week Revenue: ${report["WeekRevenue"]:F2}";
                lblAvgPrepTime.Text = $"Avg Prep Time: {report["AveragePreparationTime"]} min";

                lblPendingOrders.Text = $"Pending: {report["PendingOrders"]}";
                lblInProgressOrders.Text = $"In Progress: {report["InProgressOrders"]}";
                lblReadyOrders.Text = $"Ready: {report["ReadyOrders"]}";

                lblActiveWaiters.Text = $"Active Waiters: {report["ActiveWaiters"]}";
                lblActiveChefs.Text = $"Active Chefs: {report["ActiveChefs"]}";
                lblAvailableTables.Text = $"Available Tables: {report["AvailableTables"]}";
                lblOccupiedTables.Text = $"Occupied Tables: {report["OccupiedTables"]}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading performance data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadUsersData()
        {
            try
            {
                currentUsers = await ManagerService.GetAllUsersAsync();

                lvUsers.Items.Clear();
                foreach (var user in currentUsers)
                {
                    var listItem = new ListViewItem(user.UserID.ToString());
                    listItem.SubItems.Add(user.Name);
                    listItem.SubItems.Add(user.Username);
                    listItem.SubItems.Add(user.Role);
                    listItem.SubItems.Add(user.IsActive ? "Active" : "Inactive");
                    listItem.SubItems.Add(user.CreatedDate.ToString("MM/dd/yyyy"));
                    listItem.Tag = user;

                    // Color code by role
                    switch (user.Role.ToLower())
                    {
                        case "waiter":
                            listItem.BackColor = Color.FromArgb(220, 248, 255); // Light blue
                            break;
                        case "chef":
                            listItem.BackColor = Color.FromArgb(220, 255, 220); // Light green
                            break;
                        case "manager":
                            listItem.BackColor = Color.FromArgb(255, 248, 220); // Light yellow
                            break;
                    }

                    if (!user.IsActive)
                        listItem.ForeColor = Color.Gray;

                    lvUsers.Items.Add(listItem);
                }

                lblUsersInfo.Text = $"Total Users: {currentUsers.Count} (Waiters: {currentUsers.Count(u => u.Role == "Waiter")}, Chefs: {currentUsers.Count(u => u.Role == "Chef")})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTablesData()
        {
            try
            {
                currentTables = await ManagerService.GetAllTablesAsync();

                lvTables.Items.Clear();
                foreach (var table in currentTables)
                {
                    var listItem = new ListViewItem(table.TableID.ToString());
                    listItem.SubItems.Add(table.Capacity.ToString());
                    listItem.SubItems.Add(table.Status);
                    listItem.Tag = table;

                    // Color code by status
                    if (table.Status == "Available")
                        listItem.BackColor = Color.FromArgb(220, 255, 220); // Light green
                    else
                        listItem.BackColor = Color.FromArgb(255, 220, 220); // Light red

                    lvTables.Items.Add(listItem);
                }

                lblTablesInfo.Text = $"Total Tables: {currentTables.Count} (Available: {currentTables.Count(t => t.Status == "Available")}, Occupied: {currentTables.Count(t => t.Status == "Occupied")})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tables: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSalesReport()
        {
            try
            {
                var startDate = DateTime.Today.AddDays(-7);
                var endDate = DateTime.Today.AddDays(1);

                var report = await ManagerService.GetSalesReportAsync(startDate, endDate);

                // Update sales report
                var reportText = $"Sales Report (Last 7 Days)\n";
                reportText += $"================================\n";
                reportText += $"Total Orders: {report["TotalOrders"]}\n";
                reportText += $"Total Revenue: ${report["TotalRevenue"]:F2}\n";
                reportText += $"Average Order Value: ${report["AverageOrderValue"]:F2}\n";
                reportText += $"Total Items Sold: {report["TotalItems"]}\n\n";

                reportText += $"Top Selling Items:\n";
                reportText += $"------------------\n";
                var topItems = (List<dynamic>)report["TopSellingItems"];
                foreach (var item in topItems)
                {
                    reportText += $"• {item.GetType().GetProperty("Item")?.GetValue(item)} - {item.GetType().GetProperty("Quantity")?.GetValue(item)} sold\n";
                }

                txtSalesReport.Text = reportText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // User Management Events
        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var addUserForm = new AddUserForm();
            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                LoadUsersData();
            }
        }

        private async void btnEditUser_Click(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user to edit.", "No User Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUser = (User)lvUsers.SelectedItems[0].Tag;
            var editUserForm = new AddUserForm(selectedUser);
            if (editUserForm.ShowDialog() == DialogResult.OK)
            {
                await LoadUsersData();
            }
        }

        private async void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (lvUsers.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.", "No User Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedUser = (User)lvUsers.SelectedItems[0].Tag;

            var result = MessageBox.Show($"Are you sure you want to delete user '{selectedUser.Name}'?\n\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await ManagerService.DeleteUserAsync(selectedUser.UserID);
                    MessageBox.Show("User deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadUsersData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting user: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Table Management Events
        private void btnAddTable_Click(object sender, EventArgs e)
        {
            var addTableForm = new AddTableForm();
            if (addTableForm.ShowDialog() == DialogResult.OK)
            {
                LoadTablesData();
            }
        }

        private async void btnEditTable_Click(object sender, EventArgs e)
        {
            if (lvTables.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a table to edit.", "No Table Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedTable = (Table)lvTables.SelectedItems[0].Tag;
            var editTableForm = new AddTableForm(selectedTable);
            if (editTableForm.ShowDialog() == DialogResult.OK)
            {
                await LoadTablesData();
            }
        }

        private async void btnDeleteTable_Click(object sender, EventArgs e)
        {
            if (lvTables.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a table to delete.", "No Table Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedTable = (Table)lvTables.SelectedItems[0].Tag;

            var result = MessageBox.Show($"Are you sure you want to delete Table {selectedTable.TableID}?\n\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    await ManagerService.DeleteTableAsync(selectedTable.TableID);
                    MessageBox.Show("Table deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadTablesData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting table: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            btnRefresh.Enabled = false;
            btnRefresh.Text = "Refreshing...";

            try
            {
                await LoadPerformanceData();
                await LoadUsersData();
                await LoadTablesData();
                await LoadSalesReport();
            }
            finally
            {
                btnRefresh.Enabled = true;
                btnRefresh.Text = "Refresh All";
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                refreshTimer?.Stop();
                refreshTimer?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}