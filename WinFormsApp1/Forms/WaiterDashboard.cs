using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Forms/WaiterDashboard.cs
using FoodOrderingSystem.Services;

namespace FoodOrderingSystem.Forms
{
    public partial class WaiterDashboard : Form
    {
        public WaiterDashboard()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"Waiter Dashboard - {AuthenticationService.CurrentUser?.Name}";
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
    }

    partial class WaiterDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblWelcome;
        private Button btnLogout;
        private Label lblComingSoon;

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
            this.lblComingSoon = new Label();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblWelcome.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblWelcome.Location = new Point(50, 50);
            this.lblWelcome.Size = new Size(400, 37);
            this.lblWelcome.Text = $"Welcome, {AuthenticationService.CurrentUser?.Name}!";

            // lblComingSoon
            this.lblComingSoon.AutoSize = true;
            this.lblComingSoon.Font = new Font("Segoe UI", 14F);
            this.lblComingSoon.Location = new Point(50, 120);
            this.lblComingSoon.Size = new Size(500, 25);
            this.lblComingSoon.Text = "Order management features coming in Sprint 2...";

            // btnLogout
            this.btnLogout.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.btnLogout.BackColor = Color.FromArgb(217, 83, 79);
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnLogout.ForeColor = Color.White;
            this.btnLogout.Location = new Point(this.Width - 120, 20);
            this.btnLogout.Size = new Size(80, 35);
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);

            // WaiterDashboard
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(800, 600);
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.lblComingSoon);
            this.Controls.Add(this.btnLogout);
            this.Name = "WaiterDashboard";
            this.Text = "Waiter Dashboard";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
