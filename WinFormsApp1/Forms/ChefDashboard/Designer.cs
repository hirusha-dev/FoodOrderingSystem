using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Forms/ChefDashboard.Designer.cs
using FoodOrderingSystem.Services;

namespace FoodOrderingSystem.Forms
{
    partial class ChefDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // Header controls
        private Label lblWelcome;
        private Button btnLogout;
        private Button btnRefreshOrders;

        // Statistics panel
        private Panel pnlStatistics;
        private Label lblStatisticsTitle;
        private Label lblTodayTotal;
        private Label lblPendingCount;
        private Label lblInProgressCount;
        private Label lblReadyCount;
        private Label lblServedCount;

        // Main content
        private TabControl tabControl;
        private TabPage tabPendingOrders;
        private TabPage tabAllOrders;

        // Pending orders tab
        private ListView lvPendingOrders;
        private Button btnAcceptOrder;
        private Button btnMarkReady;
        private Button btnMarkServed;
        private Button btnViewOrderDetails;
        private Label lblPendingInfo;

        // All orders tab
        private ListView lvAllOrders;
        private Label lblAllOrdersInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new Label();
            this.btnLogout = new Button();
            this.btnRefreshOrders = new Button();
            this.pnlStatistics = new Panel();
            this.lblStatisticsTitle = new Label();
            this.lblTodayTotal = new Label();
            this.lblPendingCount = new Label();
            this.lblInProgressCount = new Label();
            this.lblReadyCount = new Label();
            this.lblServedCount = new Label();
            this.tabControl = new TabControl();
            this.tabPendingOrders = new TabPage();
            this.tabAllOrders = new TabPage();
            this.lvPendingOrders = new ListView();
            this.btnAcceptOrder = new Button();
            this.btnMarkReady = new Button();
            this.btnMarkServed = new Button();
            this.btnViewOrderDetails = new Button();
            this.lblPendingInfo = new Label();
            this.lvAllOrders = new ListView();
            this.lblAllOrdersInfo = new Label();

            this.pnlStatistics.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPendingOrders.SuspendLayout();
            this.tabAllOrders.SuspendLayout();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.FromArgb(40, 167, 69);
            this.lblWelcome.Location = new Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Text = $"Kitchen Dashboard - Chef {AuthenticationService.CurrentUser?.Name}";

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

            // btnRefreshOrders
            this.btnRefreshOrders.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.btnRefreshOrders.BackColor = Color.FromArgb(40, 167, 69);
            this.btnRefreshOrders.FlatStyle = FlatStyle.Flat;
            this.btnRefreshOrders.Font = new Font("Segoe UI", 10F);
            this.btnRefreshOrders.ForeColor = Color.White;
            this.btnRefreshOrders.Location = new Point(1010, 20);
            this.btnRefreshOrders.Name = "btnRefreshOrders";
            this.btnRefreshOrders.Size = new Size(80, 35);
            this.btnRefreshOrders.TabIndex = 49;
            this.btnRefreshOrders.Text = "Refresh";
            this.btnRefreshOrders.UseVisualStyleBackColor = false;
            this.btnRefreshOrders.Click += new EventHandler(this.btnRefreshOrders_Click);

            // pnlStatistics
            this.pnlStatistics.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
                | AnchorStyles.Right)));
            this.pnlStatistics.BackColor = Color.FromArgb(248, 249, 250);
            this.pnlStatistics.BorderStyle = BorderStyle.FixedSingle;
            this.pnlStatistics.Controls.Add(this.lblStatisticsTitle);
            this.pnlStatistics.Controls.Add(this.lblTodayTotal);
            this.pnlStatistics.Controls.Add(this.lblPendingCount);
            this.pnlStatistics.Controls.Add(this.lblInProgressCount);
            this.pnlStatistics.Controls.Add(this.lblReadyCount);
            this.pnlStatistics.Controls.Add(this.lblServedCount);
            this.pnlStatistics.Location = new Point(20, 70);
            this.pnlStatistics.Name = "pnlStatistics";
            this.pnlStatistics.Size = new Size(1160, 80);
            this.pnlStatistics.TabIndex = 1;

            // lblStatisticsTitle
            this.lblStatisticsTitle.AutoSize = true;
            this.lblStatisticsTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblStatisticsTitle.ForeColor = Color.FromArgb(73, 80, 87);
            this.lblStatisticsTitle.Location = new Point(15, 10);
            this.lblStatisticsTitle.Name = "lblStatisticsTitle";
            this.lblStatisticsTitle.Text = "Kitchen Statistics";

            // Statistics labels - arranged horizontally
            this.lblTodayTotal.AutoSize = true;
            this.lblTodayTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTodayTotal.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblTodayTotal.Location = new Point(15, 35);
            this.lblTodayTotal.Name = "lblTodayTotal";
            this.lblTodayTotal.Text = "Today's Orders: 0";

            this.lblPendingCount.AutoSize = true;
            this.lblPendingCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblPendingCount.ForeColor = Color.FromArgb(255, 193, 7);
            this.lblPendingCount.Location = new Point(200, 35);
            this.lblPendingCount.Name = "lblPendingCount";
            this.lblPendingCount.Text = "Pending: 0";

            this.lblInProgressCount.AutoSize = true;
            this.lblInProgressCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblInProgressCount.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblInProgressCount.Location = new Point(350, 35);
            this.lblInProgressCount.Name = "lblInProgressCount";
            this.lblInProgressCount.Text = "In Progress: 0";

            this.lblReadyCount.AutoSize = true;
            this.lblReadyCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblReadyCount.ForeColor = Color.FromArgb(40, 167, 69);
            this.lblReadyCount.Location = new Point(520, 35);
            this.lblReadyCount.Name = "lblReadyCount";
            this.lblReadyCount.Text = "Ready: 0";

            this.lblServedCount.AutoSize = true;
            this.lblServedCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblServedCount.ForeColor = Color.FromArgb(108, 117, 125);
            this.lblServedCount.Location = new Point(650, 35);
            this.lblServedCount.Name = "lblServedCount";
            this.lblServedCount.Text = "Served: 0";

            // tabControl
            this.tabControl.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPendingOrders);
            this.tabControl.Controls.Add(this.tabAllOrders);
            this.tabControl.Font = new Font("Segoe UI", 10F);
            this.tabControl.Location = new Point(20, 160);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(1160, 540);
            this.tabControl.TabIndex = 2;

            // tabPendingOrders
            this.tabPendingOrders.BackColor = Color.White;
            this.tabPendingOrders.Controls.Add(this.lvPendingOrders);
            this.tabPendingOrders.Controls.Add(this.btnAcceptOrder);
            this.tabPendingOrders.Controls.Add(this.btnMarkReady);
            this.tabPendingOrders.Controls.Add(this.btnMarkServed);
            this.tabPendingOrders.Controls.Add(this.btnViewOrderDetails);
            this.tabPendingOrders.Controls.Add(this.lblPendingInfo);
            this.tabPendingOrders.Location = new Point(4, 26);
            this.tabPendingOrders.Name = "tabPendingOrders";
            this.tabPendingOrders.Padding = new Padding(10);
            this.tabPendingOrders.Size = new Size(1152, 510);
            this.tabPendingOrders.TabIndex = 0;
            this.tabPendingOrders.Text = "Active Orders";

            // tabAllOrders
            this.tabAllOrders.BackColor = Color.White;
            this.tabAllOrders.Controls.Add(this.lvAllOrders);
            this.tabAllOrders.Controls.Add(this.lblAllOrdersInfo);
            this.tabAllOrders.Location = new Point(4, 26);
            this.tabAllOrders.Name = "tabAllOrders";
            this.tabAllOrders.Padding = new Padding(10);
            this.tabAllOrders.Size = new Size(1152, 510);
            this.tabAllOrders.TabIndex = 1;
            this.tabAllOrders.Text = "All Orders";

            // lvPendingOrders
            this.lvPendingOrders.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.lvPendingOrders.Font = new Font("Segoe UI", 9F);
            this.lvPendingOrders.FullRowSelect = true;
            this.lvPendingOrders.GridLines = true;
            this.lvPendingOrders.Location = new Point(10, 50);
            this.lvPendingOrders.MultiSelect = false;
            this.lvPendingOrders.Name = "lvPendingOrders";
            this.lvPendingOrders.Size = new Size(1132, 400);
            this.lvPendingOrders.TabIndex = 1;
            this.lvPendingOrders.UseCompatibleStateImageBehavior = false;
            this.lvPendingOrders.View = View.Details;
            this.lvPendingOrders.Columns.Add("Order #", 80);
            this.lvPendingOrders.Columns.Add("Table", 70);
            this.lvPendingOrders.Columns.Add("Time", 80);
            this.lvPendingOrders.Columns.Add("Elapsed", 90);
            this.lvPendingOrders.Columns.Add("Status", 100);
            this.lvPendingOrders.Columns.Add("Waiter", 120);
            this.lvPendingOrders.Columns.Add("Items", 80);

            // Action buttons for pending orders
            this.btnAcceptOrder.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnAcceptOrder.BackColor = Color.FromArgb(51, 122, 183);
            this.btnAcceptOrder.FlatStyle = FlatStyle.Flat;
            this.btnAcceptOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAcceptOrder.ForeColor = Color.White;
            this.btnAcceptOrder.Location = new Point(10, 460);
            this.btnAcceptOrder.Name = "btnAcceptOrder";
            this.btnAcceptOrder.Size = new Size(100, 35);
            this.btnAcceptOrder.TabIndex = 2;
            this.btnAcceptOrder.Text = "Accept Order";
            this.btnAcceptOrder.UseVisualStyleBackColor = false;
            this.btnAcceptOrder.Click += new EventHandler(this.btnAcceptOrder_Click);

            this.btnMarkReady.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnMarkReady.BackColor = Color.FromArgb(40, 167, 69);
            this.btnMarkReady.FlatStyle = FlatStyle.Flat;
            this.btnMarkReady.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnMarkReady.ForeColor = Color.White;
            this.btnMarkReady.Location = new Point(120, 460);
            this.btnMarkReady.Name = "btnMarkReady";
            this.btnMarkReady.Size = new Size(100, 35);
            this.btnMarkReady.TabIndex = 3;
            this.btnMarkReady.Text = "Mark Ready";
            this.btnMarkReady.UseVisualStyleBackColor = false;
            this.btnMarkReady.Click += new EventHandler(this.btnMarkReady_Click);

            this.btnMarkServed.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnMarkServed.BackColor = Color.FromArgb(108, 117, 125);
            this.btnMarkServed.FlatStyle = FlatStyle.Flat;
            this.btnMarkServed.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnMarkServed.ForeColor = Color.White;
            this.btnMarkServed.Location = new Point(230, 460);
            this.btnMarkServed.Name = "btnMarkServed";
            this.btnMarkServed.Size = new Size(100, 35);
            this.btnMarkServed.TabIndex = 4;
            this.btnMarkServed.Text = "Mark Served";
            this.btnMarkServed.UseVisualStyleBackColor = false;
            this.btnMarkServed.Click += new EventHandler(this.btnMarkServed_Click);

            this.btnViewOrderDetails.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnViewOrderDetails.BackColor = Color.FromArgb(255, 193, 7);
            this.btnViewOrderDetails.FlatStyle = FlatStyle.Flat;
            this.btnViewOrderDetails.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnViewOrderDetails.ForeColor = Color.Black;
            this.btnViewOrderDetails.Location = new Point(1020, 460);
            this.btnViewOrderDetails.Name = "btnViewOrderDetails";
            this.btnViewOrderDetails.Size = new Size(120, 35);
            this.btnViewOrderDetails.TabIndex = 5;
            this.btnViewOrderDetails.Text = "View Details";
            this.btnViewOrderDetails.UseVisualStyleBackColor = false;
            this.btnViewOrderDetails.Click += new EventHandler(this.btnViewOrderDetails_Click);

            // lblPendingInfo
            this.lblPendingInfo.AutoSize = true;
            this.lblPendingInfo.Font = new Font("Segoe UI", 9F);
            this.lblPendingInfo.ForeColor = Color.Gray;
            this.lblPendingInfo.Location = new Point(10, 15);
            this.lblPendingInfo.Name = "lblPendingInfo";
            this.lblPendingInfo.Text = "Loading active orders...";

            // lvAllOrders
            this.lvAllOrders.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.lvAllOrders.Font = new Font("Segoe UI", 9F);
            this.lvAllOrders.FullRowSelect = true;
            this.lvAllOrders.GridLines = true;
            this.lvAllOrders.Location = new Point(10, 50);
            this.lvAllOrders.MultiSelect = false;
            this.lvAllOrders.Name = "lvAllOrders";
            this.lvAllOrders.Size = new Size(1132, 450);
            this.lvAllOrders.TabIndex = 1;
            this.lvAllOrders.UseCompatibleStateImageBehavior = false;
            this.lvAllOrders.View = View.Details;
            this.lvAllOrders.Columns.Add("Order #", 80);
            this.lvAllOrders.Columns.Add("Table", 70);
            this.lvAllOrders.Columns.Add("Date/Time", 120);
            this.lvAllOrders.Columns.Add("Status", 100);
            this.lvAllOrders.Columns.Add("Waiter", 120);
            this.lvAllOrders.Columns.Add("Chef", 120);
            this.lvAllOrders.Columns.Add("Total", 80);
            this.lvAllOrders.Columns.Add("Items", 80);

            // lblAllOrdersInfo
            this.lblAllOrdersInfo.AutoSize = true;
            this.lblAllOrdersInfo.Font = new Font("Segoe UI", 9F);
            this.lblAllOrdersInfo.ForeColor = Color.Gray;
            this.lblAllOrdersInfo.Location = new Point(10, 15);
            this.lblAllOrdersInfo.Name = "lblAllOrdersInfo";
            this.lblAllOrdersInfo.Text = "Loading all orders...";

            // ChefDashboard
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1200, 720);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnRefreshOrders);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.pnlStatistics);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new Size(1000, 600);
            this.Name = "ChefDashboard";
            this.Text = "Chef Dashboard";

            this.pnlStatistics.ResumeLayout(false);
            this.pnlStatistics.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPendingOrders.ResumeLayout(false);
            this.tabPendingOrders.PerformLayout();
            this.tabAllOrders.ResumeLayout(false);
            this.tabAllOrders.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}