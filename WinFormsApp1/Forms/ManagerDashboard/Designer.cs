// Forms/ManagerDashboard.Designer.cs
using FoodOrderingSystem.Services;

namespace FoodOrderingSystem.Forms
{
    partial class ManagerDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // Header controls
        private Label lblWelcome;
        private Button btnLogout;
        private Button btnRefresh;

        // Performance panel
        private Panel pnlPerformance;
        private Label lblPerformanceTitle;
        private Label lblTodayOrders;
        private Label lblTodayRevenue;
        private Label lblWeekOrders;
        private Label lblWeekRevenue;
        private Label lblAvgPrepTime;
        private Label lblPendingOrders;
        private Label lblInProgressOrders;
        private Label lblReadyOrders;
        private Label lblActiveWaiters;
        private Label lblActiveChefs;
        private Label lblAvailableTables;
        private Label lblOccupiedTables;

        // Main content
        private TabControl tabControl;
        private TabPage tabDashboard;
        private TabPage tabUsers;
        private TabPage tabTables;
        private TabPage tabReports;

        // Users tab
        private ListView lvUsers;
        private Button btnAddUser;
        private Button btnEditUser;
        private Button btnDeleteUser;
        private Label lblUsersInfo;

        // Tables tab
        private ListView lvTables;
        private Button btnAddTable;
        private Button btnEditTable;
        private Button btnDeleteTable;
        private Label lblTablesInfo;

        // Reports tab
        private TextBox txtSalesReport;

        private void InitializeComponent()
        {
            this.lblWelcome = new Label();
            this.btnLogout = new Button();
            this.btnRefresh = new Button();
            this.pnlPerformance = new Panel();
            this.lblPerformanceTitle = new Label();
            this.lblTodayOrders = new Label();
            this.lblTodayRevenue = new Label();
            this.lblWeekOrders = new Label();
            this.lblWeekRevenue = new Label();
            this.lblAvgPrepTime = new Label();
            this.lblPendingOrders = new Label();
            this.lblInProgressOrders = new Label();
            this.lblReadyOrders = new Label();
            this.lblActiveWaiters = new Label();
            this.lblActiveChefs = new Label();
            this.lblAvailableTables = new Label();
            this.lblOccupiedTables = new Label();
            this.tabControl = new TabControl();
            this.tabDashboard = new TabPage();
            this.tabUsers = new TabPage();
            this.tabTables = new TabPage();
            this.tabReports = new TabPage();
            this.lvUsers = new ListView();
            this.btnAddUser = new Button();
            this.btnEditUser = new Button();
            this.btnDeleteUser = new Button();
            this.lblUsersInfo = new Label();
            this.lvTables = new ListView();
            this.btnAddTable = new Button();
            this.btnEditTable = new Button();
            this.btnDeleteTable = new Button();
            this.lblTablesInfo = new Label();
            this.txtSalesReport = new TextBox();

            this.pnlPerformance.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabUsers.SuspendLayout();
            this.tabTables.SuspendLayout();
            this.tabReports.SuspendLayout();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.FromArgb(255, 193, 7);
            this.lblWelcome.Location = new Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Text = $"Manager Dashboard - {AuthenticationService.CurrentUser?.Name}";

            // btnLogout
            this.btnLogout.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.btnLogout.BackColor = Color.FromArgb(217, 83, 79);
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogout.ForeColor = Color.White;
            this.btnLogout.Location = new Point(1100, 20);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new Size(80, 35);
            this.btnLogout.TabIndex = 50;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);

            // btnRefresh
            this.btnRefresh.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.btnRefresh.BackColor = Color.FromArgb(40, 167, 69);
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.Font = new Font("Segoe UI", 10F);
            this.btnRefresh.ForeColor = Color.White;
            this.btnRefresh.Location = new Point(1000, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(90, 35);
            this.btnRefresh.TabIndex = 49;
            this.btnRefresh.Text = "Refresh All";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // pnlPerformance
            this.pnlPerformance.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
                | AnchorStyles.Right)));
            this.pnlPerformance.BackColor = Color.FromArgb(248, 249, 250);
            this.pnlPerformance.BorderStyle = BorderStyle.FixedSingle;
            this.pnlPerformance.Controls.Add(this.lblPerformanceTitle);
            this.pnlPerformance.Controls.Add(this.lblTodayOrders);
            this.pnlPerformance.Controls.Add(this.lblTodayRevenue);
            this.pnlPerformance.Controls.Add(this.lblWeekOrders);
            this.pnlPerformance.Controls.Add(this.lblWeekRevenue);
            this.pnlPerformance.Controls.Add(this.lblAvgPrepTime);
            this.pnlPerformance.Controls.Add(this.lblPendingOrders);
            this.pnlPerformance.Controls.Add(this.lblInProgressOrders);
            this.pnlPerformance.Controls.Add(this.lblReadyOrders);
            this.pnlPerformance.Controls.Add(this.lblActiveWaiters);
            this.pnlPerformance.Controls.Add(this.lblActiveChefs);
            this.pnlPerformance.Controls.Add(this.lblAvailableTables);
            this.pnlPerformance.Controls.Add(this.lblOccupiedTables);
            this.pnlPerformance.Location = new Point(20, 70);
            this.pnlPerformance.Name = "pnlPerformance";
            this.pnlPerformance.Size = new Size(1160, 120);
            this.pnlPerformance.TabIndex = 1;

            // lblPerformanceTitle
            this.lblPerformanceTitle.AutoSize = true;
            this.lblPerformanceTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblPerformanceTitle.ForeColor = Color.FromArgb(73, 80, 87);
            this.lblPerformanceTitle.Location = new Point(15, 10);
            this.lblPerformanceTitle.Name = "lblPerformanceTitle";
            this.lblPerformanceTitle.Text = "Restaurant Performance Overview";

            // Performance metrics - arranged in rows
            // Row 1: Sales metrics
            this.lblTodayOrders.AutoSize = true;
            this.lblTodayOrders.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTodayOrders.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblTodayOrders.Location = new Point(15, 40);
            this.lblTodayOrders.Name = "lblTodayOrders";
            this.lblTodayOrders.Text = "Today's Orders: 0";

            this.lblTodayRevenue.AutoSize = true;
            this.lblTodayRevenue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTodayRevenue.ForeColor = Color.FromArgb(40, 167, 69);
            this.lblTodayRevenue.Location = new Point(200, 40);
            this.lblTodayRevenue.Name = "lblTodayRevenue";
            this.lblTodayRevenue.Text = "Today's Revenue: $0.00";

            this.lblWeekOrders.AutoSize = true;
            this.lblWeekOrders.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblWeekOrders.ForeColor = Color.FromArgb(255, 193, 7);
            this.lblWeekOrders.Location = new Point(400, 40);
            this.lblWeekOrders.Name = "lblWeekOrders";
            this.lblWeekOrders.Text = "This Week: 0 orders";

            this.lblWeekRevenue.AutoSize = true;
            this.lblWeekRevenue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblWeekRevenue.ForeColor = Color.FromArgb(40, 167, 69);
            this.lblWeekRevenue.Location = new Point(600, 40);
            this.lblWeekRevenue.Name = "lblWeekRevenue";
            this.lblWeekRevenue.Text = "Week Revenue: $0.00";

            this.lblAvgPrepTime.AutoSize = true;
            this.lblAvgPrepTime.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblAvgPrepTime.ForeColor = Color.FromArgb(108, 117, 125);
            this.lblAvgPrepTime.Location = new Point(820, 40);
            this.lblAvgPrepTime.Name = "lblAvgPrepTime";
            this.lblAvgPrepTime.Text = "Avg Prep Time: 0 min";

            // Row 2: Order status
            this.lblPendingOrders.AutoSize = true;
            this.lblPendingOrders.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblPendingOrders.ForeColor = Color.FromArgb(255, 193, 7);
            this.lblPendingOrders.Location = new Point(15, 65);
            this.lblPendingOrders.Name = "lblPendingOrders";
            this.lblPendingOrders.Text = "Pending: 0";

            this.lblInProgressOrders.AutoSize = true;
            this.lblInProgressOrders.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblInProgressOrders.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblInProgressOrders.Location = new Point(150, 65);
            this.lblInProgressOrders.Name = "lblInProgressOrders";
            this.lblInProgressOrders.Text = "In Progress: 0";

            this.lblReadyOrders.AutoSize = true;
            this.lblReadyOrders.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblReadyOrders.ForeColor = Color.FromArgb(40, 167, 69);
            this.lblReadyOrders.Location = new Point(320, 65);
            this.lblReadyOrders.Name = "lblReadyOrders";
            this.lblReadyOrders.Text = "Ready: 0";

            // Row 3: Staff and tables
            this.lblActiveWaiters.AutoSize = true;
            this.lblActiveWaiters.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblActiveWaiters.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblActiveWaiters.Location = new Point(15, 90);
            this.lblActiveWaiters.Name = "lblActiveWaiters";
            this.lblActiveWaiters.Text = "Active Waiters: 0";

            this.lblActiveChefs.AutoSize = true;
            this.lblActiveChefs.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblActiveChefs.ForeColor = Color.FromArgb(40, 167, 69);
            this.lblActiveChefs.Location = new Point(200, 90);
            this.lblActiveChefs.Name = "lblActiveChefs";
            this.lblActiveChefs.Text = "Active Chefs: 0";

            this.lblAvailableTables.AutoSize = true;
            this.lblAvailableTables.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblAvailableTables.ForeColor = Color.FromArgb(40, 167, 69);
            this.lblAvailableTables.Location = new Point(350, 90);
            this.lblAvailableTables.Name = "lblAvailableTables";
            this.lblAvailableTables.Text = "Available Tables: 0";

            this.lblOccupiedTables.AutoSize = true;
            this.lblOccupiedTables.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblOccupiedTables.ForeColor = Color.FromArgb(217, 83, 79);
            this.lblOccupiedTables.Location = new Point(550, 90);
            this.lblOccupiedTables.Name = "lblOccupiedTables";
            this.lblOccupiedTables.Text = "Occupied Tables: 0";

            // tabControl
            this.tabControl.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabDashboard);
            this.tabControl.Controls.Add(this.tabUsers);
            this.tabControl.Controls.Add(this.tabTables);
            this.tabControl.Controls.Add(this.tabReports);
            this.tabControl.Font = new Font("Segoe UI", 10F);
            this.tabControl.Location = new Point(20, 200);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(1160, 500);
            this.tabControl.TabIndex = 2;

            // tabDashboard
            this.tabDashboard.BackColor = Color.White;
            this.tabDashboard.Location = new Point(4, 26);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new Padding(10);
            this.tabDashboard.Size = new Size(1152, 470);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "Dashboard";

            // tabUsers
            this.tabUsers.BackColor = Color.White;
            this.tabUsers.Controls.Add(this.lvUsers);
            this.tabUsers.Controls.Add(this.btnAddUser);
            this.tabUsers.Controls.Add(this.btnEditUser);
            this.tabUsers.Controls.Add(this.btnDeleteUser);
            this.tabUsers.Controls.Add(this.lblUsersInfo);
            this.tabUsers.Location = new Point(4, 26);
            this.tabUsers.Name = "tabUsers";
            this.tabUsers.Padding = new Padding(10);
            this.tabUsers.Size = new Size(1152, 470);
            this.tabUsers.TabIndex = 1;
            this.tabUsers.Text = "User Management";

            // tabTables
            this.tabTables.BackColor = Color.White;
            this.tabTables.Controls.Add(this.lvTables);
            this.tabTables.Controls.Add(this.btnAddTable);
            this.tabTables.Controls.Add(this.btnEditTable);
            this.tabTables.Controls.Add(this.btnDeleteTable);
            this.tabTables.Controls.Add(this.lblTablesInfo);
            this.tabTables.Location = new Point(4, 26);
            this.tabTables.Name = "tabTables";
            this.tabTables.Padding = new Padding(10);
            this.tabTables.Size = new Size(1152, 470);
            this.tabTables.TabIndex = 2;
            this.tabTables.Text = "Table Management";

            // tabReports
            this.tabReports.BackColor = Color.White;
            this.tabReports.Controls.Add(this.txtSalesReport);
            this.tabReports.Location = new Point(4, 26);
            this.tabReports.Name = "tabReports";
            this.tabReports.Padding = new Padding(10);
            this.tabReports.Size = new Size(1152, 470);
            this.tabReports.TabIndex = 3;
            this.tabReports.Text = "Reports";

            // lvUsers
            this.lvUsers.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.lvUsers.Font = new Font("Segoe UI", 9F);
            this.lvUsers.FullRowSelect = true;
            this.lvUsers.GridLines = true;
            this.lvUsers.Location = new Point(10, 50);
            this.lvUsers.MultiSelect = false;
            this.lvUsers.Name = "lvUsers";
            this.lvUsers.Size = new Size(1132, 360);
            this.lvUsers.TabIndex = 1;
            this.lvUsers.UseCompatibleStateImageBehavior = false;
            this.lvUsers.View = View.Details;
            this.lvUsers.Columns.Add("ID", 50);
            this.lvUsers.Columns.Add("Name", 200);
            this.lvUsers.Columns.Add("Username", 150);
            this.lvUsers.Columns.Add("Role", 100);
            this.lvUsers.Columns.Add("Status", 100);
            this.lvUsers.Columns.Add("Created", 120);

            // User management buttons
            this.btnAddUser.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnAddUser.BackColor = Color.FromArgb(40, 167, 69);
            this.btnAddUser.FlatStyle = FlatStyle.Flat;
            this.btnAddUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAddUser.ForeColor = Color.White;
            this.btnAddUser.Location = new Point(10, 420);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new Size(100, 35);
            this.btnAddUser.TabIndex = 2;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.UseVisualStyleBackColor = false;
            this.btnAddUser.Click += new EventHandler(this.btnAddUser_Click);

            this.btnEditUser.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnEditUser.BackColor = Color.FromArgb(255, 193, 7);
            this.btnEditUser.FlatStyle = FlatStyle.Flat;
            this.btnEditUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEditUser.ForeColor = Color.Black;
            this.btnEditUser.Location = new Point(120, 420);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new Size(100, 35);
            this.btnEditUser.TabIndex = 3;
            this.btnEditUser.Text = "Edit User";
            this.btnEditUser.UseVisualStyleBackColor = false;
            this.btnEditUser.Click += new EventHandler(this.btnEditUser_Click);

            this.btnDeleteUser.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnDeleteUser.BackColor = Color.FromArgb(217, 83, 79);
            this.btnDeleteUser.FlatStyle = FlatStyle.Flat;
            this.btnDeleteUser.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnDeleteUser.ForeColor = Color.White;
            this.btnDeleteUser.Location = new Point(230, 420);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new Size(100, 35);
            this.btnDeleteUser.TabIndex = 4;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = false;
            this.btnDeleteUser.Click += new EventHandler(this.btnDeleteUser_Click);

            // lblUsersInfo
            this.lblUsersInfo.AutoSize = true;
            this.lblUsersInfo.Font = new Font("Segoe UI", 9F);
            this.lblUsersInfo.ForeColor = Color.Gray;
            this.lblUsersInfo.Location = new Point(10, 15);
            this.lblUsersInfo.Name = "lblUsersInfo";
            this.lblUsersInfo.Text = "Loading users...";

            // lvTables
            this.lvTables.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.lvTables.Font = new Font("Segoe UI", 9F);
            this.lvTables.FullRowSelect = true;
            this.lvTables.GridLines = true;
            this.lvTables.Location = new Point(10, 50);
            this.lvTables.MultiSelect = false;
            this.lvTables.Name = "lvTables";
            this.lvTables.Size = new Size(1132, 360);
            this.lvTables.TabIndex = 1;
            this.lvTables.UseCompatibleStateImageBehavior = false;
            this.lvTables.View = View.Details;
            this.lvTables.Columns.Add("Table ID", 100);
            this.lvTables.Columns.Add("Capacity", 100);
            this.lvTables.Columns.Add("Status", 150);

            // Table management buttons
            this.btnAddTable.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnAddTable.BackColor = Color.FromArgb(40, 167, 69);
            this.btnAddTable.FlatStyle = FlatStyle.Flat;
            this.btnAddTable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAddTable.ForeColor = Color.White;
            this.btnAddTable.Location = new Point(10, 420);
            this.btnAddTable.Name = "btnAddTable";
            this.btnAddTable.Size = new Size(100, 35);
            this.btnAddTable.TabIndex = 2;
            this.btnAddTable.Text = "Add Table";
            this.btnAddTable.UseVisualStyleBackColor = false;
            this.btnAddTable.Click += new EventHandler(this.btnAddTable_Click);

            this.btnEditTable.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnEditTable.BackColor = Color.FromArgb(255, 193, 7);
            this.btnEditTable.FlatStyle = FlatStyle.Flat;
            this.btnEditTable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnEditTable.ForeColor = Color.Black;
            this.btnEditTable.Location = new Point(120, 420);
            this.btnEditTable.Name = "btnEditTable";
            this.btnEditTable.Size = new Size(100, 35);
            this.btnEditTable.TabIndex = 3;
            this.btnEditTable.Text = "Edit Table";
            this.btnEditTable.UseVisualStyleBackColor = false;
            this.btnEditTable.Click += new EventHandler(this.btnEditTable_Click);

            this.btnDeleteTable.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnDeleteTable.BackColor = Color.FromArgb(217, 83, 79);
            this.btnDeleteTable.FlatStyle = FlatStyle.Flat;
            this.btnDeleteTable.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnDeleteTable.ForeColor = Color.White;
            this.btnDeleteTable.Location = new Point(230, 420);
            this.btnDeleteTable.Name = "btnDeleteTable";
            this.btnDeleteTable.Size = new Size(100, 35);
            this.btnDeleteTable.TabIndex = 4;
            this.btnDeleteTable.Text = "Delete Table";
            this.btnDeleteTable.UseVisualStyleBackColor = false;
            this.btnDeleteTable.Click += new EventHandler(this.btnDeleteTable_Click);

            // lblTablesInfo
            this.lblTablesInfo.AutoSize = true;
            this.lblTablesInfo.Font = new Font("Segoe UI", 9F);
            this.lblTablesInfo.ForeColor = Color.Gray;
            this.lblTablesInfo.Location = new Point(10, 15);
            this.lblTablesInfo.Name = "lblTablesInfo";
            this.lblTablesInfo.Text = "Loading tables...";

            // txtSalesReport
            this.txtSalesReport.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.txtSalesReport.Font = new Font("Consolas", 10F);
            this.txtSalesReport.Location = new Point(10, 10);
            this.txtSalesReport.Multiline = true;
            this.txtSalesReport.Name = "txtSalesReport";
            this.txtSalesReport.ReadOnly = true;
            this.txtSalesReport.ScrollBars = ScrollBars.Vertical;
            this.txtSalesReport.Size = new Size(1132, 450);
            this.txtSalesReport.TabIndex = 0;
            this.txtSalesReport.Text = "Loading sales report...";

            // ManagerDashboard
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1200, 720);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.pnlPerformance);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new Size(1000, 600);
            this.Name = "ManagerDashboard";
            this.Text = "Manager Dashboard";

            this.pnlPerformance.ResumeLayout(false);
            this.pnlPerformance.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabUsers.ResumeLayout(false);
            this.tabUsers.PerformLayout();
            this.tabTables.ResumeLayout(false);
            this.tabTables.PerformLayout();
            this.tabReports.ResumeLayout(false);
            this.tabReports.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}