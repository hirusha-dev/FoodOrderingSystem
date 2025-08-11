// Forms/WaiterDashboard.Designer.cs - RESPONSIVE VERSION
using FoodOrderingSystem.Services;

namespace FoodOrderingSystem.Forms
{
    partial class WaiterDashboard
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private Label lblWelcome;
        private Button btnLogout;
        private Button btnRefresh;
        private TabControl tabControl;
        private TabPage tabNewOrder;
        private TabPage tabOrderHistory;

        // Menu section
        private GroupBox gbMenu;
        private ComboBox cmbCategory;
        private ListView lvMenu;
        private Label lblCategory;

        // Order building section
        private GroupBox gbOrderBuilder;
        private NumericUpDown nudQuantity;
        private TextBox txtSpecialInstructions;
        private Button btnAddToOrder;
        private Label lblQuantity;
        private Label lblSpecialInstructions;

        // Current order section
        private GroupBox gbCurrentOrder;
        private ListView lvCurrentOrder;
        private Button btnRemoveFromOrder;
        private Button btnClearOrder;
        private ComboBox cmbTable;
        private Button btnSubmitOrder;
        private Label lblTable;
        private Label lblOrderTotal;

        // Order history components
        private ListView lvOrderHistory;
        private Button btnRefreshHistory;
        private Label lblHistoryInfo;

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
            this.btnRefresh = new Button();
            this.tabControl = new TabControl();
            this.tabNewOrder = new TabPage();
            this.tabOrderHistory = new TabPage();
            this.gbMenu = new GroupBox();
            this.cmbCategory = new ComboBox();
            this.lvMenu = new ListView();
            this.lblCategory = new Label();
            this.gbOrderBuilder = new GroupBox();
            this.nudQuantity = new NumericUpDown();
            this.txtSpecialInstructions = new TextBox();
            this.btnAddToOrder = new Button();
            this.lblQuantity = new Label();
            this.lblSpecialInstructions = new Label();
            this.gbCurrentOrder = new GroupBox();
            this.lvCurrentOrder = new ListView();
            this.btnRemoveFromOrder = new Button();
            this.btnClearOrder = new Button();
            this.cmbTable = new ComboBox();
            this.btnSubmitOrder = new Button();
            this.lblTable = new Label();
            this.lblOrderTotal = new Label();

            this.tabControl.SuspendLayout();
            this.tabNewOrder.SuspendLayout();
            this.gbMenu.SuspendLayout();
            this.gbOrderBuilder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.gbCurrentOrder.SuspendLayout();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblWelcome.Location = new Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Text = $"Welcome, {AuthenticationService.CurrentUser?.Name}!";

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
            this.btnRefresh.Location = new Point(1010, 20);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new Size(80, 35);
            this.btnRefresh.TabIndex = 49;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // tabControl
            this.tabControl.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabNewOrder);
            this.tabControl.Controls.Add(this.tabOrderHistory);
            this.tabControl.Font = new Font("Segoe UI", 10F);
            this.tabControl.Location = new Point(20, 70);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(1160, 630);
            this.tabControl.TabIndex = 0;

            // tabNewOrder
            this.tabNewOrder.BackColor = Color.White;
            this.tabNewOrder.Controls.Add(this.gbMenu);
            this.tabNewOrder.Controls.Add(this.gbOrderBuilder);
            this.tabNewOrder.Controls.Add(this.gbCurrentOrder);
            this.tabNewOrder.Location = new Point(4, 26);
            this.tabNewOrder.Name = "tabNewOrder";
            this.tabNewOrder.Padding = new Padding(10);
            this.tabNewOrder.Size = new Size(1152, 600);
            this.tabNewOrder.TabIndex = 0;
            this.tabNewOrder.Text = "New Order";

            // tabOrderHistory
            this.tabOrderHistory.BackColor = Color.White;
            this.tabOrderHistory.Location = new Point(4, 26);
            this.tabOrderHistory.Name = "tabOrderHistory";
            this.tabOrderHistory.Padding = new Padding(10);
            this.tabOrderHistory.Size = new Size(1152, 600);
            this.tabOrderHistory.TabIndex = 1;
            this.tabOrderHistory.Text = "Order History";

            // gbMenu - RESPONSIVE: Takes 60% of width
            this.gbMenu.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.gbMenu.Controls.Add(this.lblCategory);
            this.gbMenu.Controls.Add(this.cmbCategory);
            this.gbMenu.Controls.Add(this.lvMenu);
            this.gbMenu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.gbMenu.Location = new Point(10, 10);
            this.gbMenu.Name = "gbMenu";
            this.gbMenu.Size = new Size(690, 580); // 60% of 1152px
            this.gbMenu.TabIndex = 1;
            this.gbMenu.TabStop = false;
            this.gbMenu.Text = "Menu Items";

            // lblCategory
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new Font("Segoe UI", 9F);
            this.lblCategory.Location = new Point(10, 25);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new Size(55, 15);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Category:";

            // cmbCategory
            this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new Font("Segoe UI", 9F);
            this.cmbCategory.Location = new Point(10, 45);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new Size(250, 23);
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.SelectedIndexChanged += new EventHandler(this.cmbCategory_SelectedIndexChanged);

            // lvMenu - RESPONSIVE: Full width within groupbox
            this.lvMenu.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.lvMenu.Font = new Font("Segoe UI", 9F);
            this.lvMenu.FullRowSelect = true;
            this.lvMenu.GridLines = true;
            this.lvMenu.Location = new Point(10, 80);
            this.lvMenu.MultiSelect = false;
            this.lvMenu.Name = "lvMenu";
            this.lvMenu.Size = new Size(670, 490); // Full width minus margins
            this.lvMenu.TabIndex = 2;
            this.lvMenu.UseCompatibleStateImageBehavior = false;
            this.lvMenu.View = View.Details;
            this.lvMenu.Columns.Add("Item Name", 200);
            this.lvMenu.Columns.Add("Category", 120);
            this.lvMenu.Columns.Add("Price", 80);
            this.lvMenu.Columns.Add("Description", 270);

            // Set row height to accommodate images (64px + padding)
            this.lvMenu.OwnerDraw = false; // Use default drawing for better image support

            // gbOrderBuilder - RESPONSIVE: Fixed right position, 40% of width
            this.gbOrderBuilder.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.gbOrderBuilder.Controls.Add(this.lblQuantity);
            this.gbOrderBuilder.Controls.Add(this.nudQuantity);
            this.gbOrderBuilder.Controls.Add(this.lblSpecialInstructions);
            this.gbOrderBuilder.Controls.Add(this.txtSpecialInstructions);
            this.gbOrderBuilder.Controls.Add(this.btnAddToOrder);
            this.gbOrderBuilder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.gbOrderBuilder.Location = new Point(710, 10);
            this.gbOrderBuilder.Name = "gbOrderBuilder";
            this.gbOrderBuilder.Size = new Size(432, 150); // 40% of 1152px minus margins
            this.gbOrderBuilder.TabIndex = 2;
            this.gbOrderBuilder.TabStop = false;
            this.gbOrderBuilder.Text = "Add to Order";

            // lblQuantity
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new Font("Segoe UI", 9F);
            this.lblQuantity.Location = new Point(10, 25);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new Size(56, 15);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Quantity:";

            // nudQuantity
            this.nudQuantity.Font = new Font("Segoe UI", 9F);
            this.nudQuantity.Location = new Point(10, 45);
            this.nudQuantity.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            this.nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new Size(80, 23);
            this.nudQuantity.TabIndex = 1;
            this.nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // lblSpecialInstructions
            this.lblSpecialInstructions.AutoSize = true;
            this.lblSpecialInstructions.Font = new Font("Segoe UI", 9F);
            this.lblSpecialInstructions.Location = new Point(10, 75);
            this.lblSpecialInstructions.Name = "lblSpecialInstructions";
            this.lblSpecialInstructions.Size = new Size(115, 15);
            this.lblSpecialInstructions.TabIndex = 2;
            this.lblSpecialInstructions.Text = "Special Instructions:";

            // txtSpecialInstructions - RESPONSIVE: Stretch width
            this.txtSpecialInstructions.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Left)
                | AnchorStyles.Right)));
            this.txtSpecialInstructions.Font = new Font("Segoe UI", 9F);
            this.txtSpecialInstructions.Location = new Point(10, 95);
            this.txtSpecialInstructions.Name = "txtSpecialInstructions";
            this.txtSpecialInstructions.Size = new Size(330, 23);
            this.txtSpecialInstructions.TabIndex = 3;

            // btnAddToOrder
            this.btnAddToOrder.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.btnAddToOrder.BackColor = Color.FromArgb(40, 167, 69);
            this.btnAddToOrder.FlatStyle = FlatStyle.Flat;
            this.btnAddToOrder.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.btnAddToOrder.ForeColor = Color.White;
            this.btnAddToOrder.Location = new Point(350, 95);
            this.btnAddToOrder.Name = "btnAddToOrder";
            this.btnAddToOrder.Size = new Size(70, 23);
            this.btnAddToOrder.TabIndex = 4;
            this.btnAddToOrder.Text = "Add";
            this.btnAddToOrder.UseVisualStyleBackColor = false;
            this.btnAddToOrder.Click += new EventHandler(this.btnAddToOrder_Click);

            // gbCurrentOrder - RESPONSIVE: Anchored to right and bottom
            this.gbCurrentOrder.Anchor = ((AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Right)));
            this.gbCurrentOrder.Controls.Add(this.lvCurrentOrder);
            this.gbCurrentOrder.Controls.Add(this.btnRemoveFromOrder);
            this.gbCurrentOrder.Controls.Add(this.btnClearOrder);
            this.gbCurrentOrder.Controls.Add(this.lblTable);
            this.gbCurrentOrder.Controls.Add(this.cmbTable);
            this.gbCurrentOrder.Controls.Add(this.lblOrderTotal);
            this.gbCurrentOrder.Controls.Add(this.btnSubmitOrder);
            this.gbCurrentOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.gbCurrentOrder.Location = new Point(710, 170);
            this.gbCurrentOrder.Name = "gbCurrentOrder";
            this.gbCurrentOrder.Size = new Size(432, 420); // Stretch to bottom
            this.gbCurrentOrder.TabIndex = 3;
            this.gbCurrentOrder.TabStop = false;
            this.gbCurrentOrder.Text = "Current Order";

            // lvCurrentOrder - RESPONSIVE: Stretch within groupbox
            this.lvCurrentOrder.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.lvCurrentOrder.Font = new Font("Segoe UI", 8F);
            this.lvCurrentOrder.FullRowSelect = true;
            this.lvCurrentOrder.GridLines = true;
            this.lvCurrentOrder.Location = new Point(10, 25);
            this.lvCurrentOrder.MultiSelect = false;
            this.lvCurrentOrder.Name = "lvCurrentOrder";
            this.lvCurrentOrder.Size = new Size(412, 280); // Responsive height
            this.lvCurrentOrder.TabIndex = 0;
            this.lvCurrentOrder.UseCompatibleStateImageBehavior = false;
            this.lvCurrentOrder.View = View.Details;
            this.lvCurrentOrder.Columns.Add("Item", 120);
            this.lvCurrentOrder.Columns.Add("Qty", 40);
            this.lvCurrentOrder.Columns.Add("Price", 60);
            this.lvCurrentOrder.Columns.Add("Total", 70);
            this.lvCurrentOrder.Columns.Add("Notes", 122);

            // btnRemoveFromOrder
            this.btnRemoveFromOrder.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnRemoveFromOrder.BackColor = Color.FromArgb(217, 83, 79);
            this.btnRemoveFromOrder.FlatStyle = FlatStyle.Flat;
            this.btnRemoveFromOrder.Font = new Font("Segoe UI", 8F);
            this.btnRemoveFromOrder.ForeColor = Color.White;
            this.btnRemoveFromOrder.Location = new Point(10, 315);
            this.btnRemoveFromOrder.Name = "btnRemoveFromOrder";
            this.btnRemoveFromOrder.Size = new Size(70, 25);
            this.btnRemoveFromOrder.TabIndex = 1;
            this.btnRemoveFromOrder.Text = "Remove";
            this.btnRemoveFromOrder.UseVisualStyleBackColor = false;
            this.btnRemoveFromOrder.Click += new EventHandler(this.btnRemoveFromOrder_Click);

            // btnClearOrder
            this.btnClearOrder.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.btnClearOrder.BackColor = Color.FromArgb(108, 117, 125);
            this.btnClearOrder.FlatStyle = FlatStyle.Flat;
            this.btnClearOrder.Font = new Font("Segoe UI", 8F);
            this.btnClearOrder.ForeColor = Color.White;
            this.btnClearOrder.Location = new Point(90, 315);
            this.btnClearOrder.Name = "btnClearOrder";
            this.btnClearOrder.Size = new Size(50, 25);
            this.btnClearOrder.TabIndex = 2;
            this.btnClearOrder.Text = "Clear";
            this.btnClearOrder.UseVisualStyleBackColor = false;
            this.btnClearOrder.Click += new EventHandler(this.btnClearOrder_Click);

            // lblTable
            this.lblTable.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new Font("Segoe UI", 9F);
            this.lblTable.Location = new Point(10, 350);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new Size(35, 15);
            this.lblTable.TabIndex = 3;
            this.lblTable.Text = "Table:";

            // cmbTable
            this.cmbTable.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.cmbTable.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTable.Font = new Font("Segoe UI", 9F);
            this.cmbTable.Location = new Point(50, 347);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new Size(150, 23);
            this.cmbTable.TabIndex = 4;

            // lblOrderTotal
            this.lblOrderTotal.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.lblOrderTotal.AutoSize = true;
            this.lblOrderTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblOrderTotal.ForeColor = Color.FromArgb(40, 167, 69);
            this.lblOrderTotal.Location = new Point(10, 380);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.Size = new Size(120, 19);
            this.lblOrderTotal.TabIndex = 5;
            this.lblOrderTotal.Text = "Order Total: LKR 0.00";

            // btnSubmitOrder
            this.btnSubmitOrder.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.btnSubmitOrder.BackColor = Color.FromArgb(51, 122, 183);
            this.btnSubmitOrder.Enabled = false;
            this.btnSubmitOrder.FlatStyle = FlatStyle.Flat;
            this.btnSubmitOrder.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSubmitOrder.ForeColor = Color.White;
            this.btnSubmitOrder.Location = new Point(290, 345);
            this.btnSubmitOrder.Name = "btnSubmitOrder";
            this.btnSubmitOrder.Size = new Size(130, 60);
            this.btnSubmitOrder.TabIndex = 6;
            this.btnSubmitOrder.Text = "Submit Order";
            this.btnSubmitOrder.UseVisualStyleBackColor = false;
            this.btnSubmitOrder.Click += new EventHandler(this.btnSubmitOrder_Click);

            // WaiterDashboard
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.ClientSize = new Size(1200, 720);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new Size(1000, 600);
            this.Name = "WaiterDashboard";
            this.Text = "Waiter Dashboard";

            this.tabControl.ResumeLayout(false);
            this.tabNewOrder.ResumeLayout(false);
            this.gbMenu.ResumeLayout(false);
            this.gbMenu.PerformLayout();
            this.gbOrderBuilder.ResumeLayout(false);
            this.gbOrderBuilder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.gbCurrentOrder.ResumeLayout(false);
            this.gbCurrentOrder.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

            InitializeOrderHistoryTab();
        }

        // Add this method to handle order history refresh:
        private void btnRefreshHistory_Click(object sender, EventArgs e)
        {
            LoadOrderHistory();
        }

        private void InitializeOrderHistoryTab()
        {
            this.lvOrderHistory = new ListView();
            this.btnRefreshHistory = new Button();
            this.lblHistoryInfo = new Label();

            // lvOrderHistory - RESPONSIVE: Full width and height
            this.lvOrderHistory.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
                | AnchorStyles.Left) | AnchorStyles.Right)));
            this.lvOrderHistory.Font = new Font("Segoe UI", 9F);
            this.lvOrderHistory.FullRowSelect = true;
            this.lvOrderHistory.GridLines = true;
            this.lvOrderHistory.Location = new Point(10, 50);
            this.lvOrderHistory.MultiSelect = false;
            this.lvOrderHistory.Name = "lvOrderHistory";
            this.lvOrderHistory.Size = new Size(1132, 540); // Full width minus margins
            this.lvOrderHistory.TabIndex = 1;
            this.lvOrderHistory.UseCompatibleStateImageBehavior = false;
            this.lvOrderHistory.View = View.Details;
            this.lvOrderHistory.Columns.Add("Order #", 100);
            this.lvOrderHistory.Columns.Add("Table", 80);
            this.lvOrderHistory.Columns.Add("Date & Time", 150);
            this.lvOrderHistory.Columns.Add("Status", 120);
            this.lvOrderHistory.Columns.Add("Total", 100);
            this.lvOrderHistory.Columns.Add("Items", 100);
            this.lvOrderHistory.Columns.Add("Details", 582); // Remaining width

            // btnRefreshHistory
            this.btnRefreshHistory.BackColor = Color.FromArgb(40, 167, 69);
            this.btnRefreshHistory.FlatStyle = FlatStyle.Flat;
            this.btnRefreshHistory.Font = new Font("Segoe UI", 9F);
            this.btnRefreshHistory.ForeColor = Color.White;
            this.btnRefreshHistory.Location = new Point(10, 10);
            this.btnRefreshHistory.Name = "btnRefreshHistory";
            this.btnRefreshHistory.Size = new Size(120, 30);
            this.btnRefreshHistory.TabIndex = 0;
            this.btnRefreshHistory.Text = "Refresh History";
            this.btnRefreshHistory.UseVisualStyleBackColor = false;
            this.btnRefreshHistory.Click += new EventHandler(this.btnRefreshHistory_Click);

            // lblHistoryInfo
            this.lblHistoryInfo.AutoSize = true;
            this.lblHistoryInfo.Font = new Font("Segoe UI", 9F);
            this.lblHistoryInfo.ForeColor = Color.Gray;
            this.lblHistoryInfo.Location = new Point(140, 18);
            this.lblHistoryInfo.Name = "lblHistoryInfo";
            this.lblHistoryInfo.Size = new Size(200, 15);
            this.lblHistoryInfo.TabIndex = 2;
            this.lblHistoryInfo.Text = "Loading order history...";

            // Add controls to the tab
            this.tabOrderHistory.Controls.Add(this.lvOrderHistory);
            this.tabOrderHistory.Controls.Add(this.btnRefreshHistory);
            this.tabOrderHistory.Controls.Add(this.lblHistoryInfo);
        }
    }
}