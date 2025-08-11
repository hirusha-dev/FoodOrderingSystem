// Forms/AddUserForm.cs
using FoodOrderingSystem.Services;
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Forms
{
    public partial class AddUserForm : Form
    {
        private User? editingUser;
        private bool isEditMode;

        public AddUserForm(User? user = null)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            editingUser = user;
            isEditMode = user != null;

            if (isEditMode)
            {
                this.Text = "Edit User";
                lblTitle.Text = "Edit User";
                btnSave.Text = "Update User";
                LoadUserData();
            }
            else
            {
                this.Text = "Add New User";
                lblTitle.Text = "Add New User";
                btnSave.Text = "Create User";
            }

            // Populate role combo box
            cmbRole.Items.AddRange(new string[] { "Waiter", "Chef", "Manager" });
            cmbRole.SelectedIndex = 0;
        }

        private void LoadUserData()
        {
            if (editingUser != null)
            {
                txtName.Text = editingUser.Name;
                txtUsername.Text = editingUser.Username;
                txtPassword.Text = editingUser.Password;
                cmbRole.SelectedItem = editingUser.Role;
                chkIsActive.Checked = editingUser.IsActive;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                btnSave.Enabled = false;
                btnSave.Text = isEditMode ? "Updating..." : "Creating...";

                bool success;

                if (isEditMode)
                {
                    success = await ManagerService.UpdateUserAsync(
                        editingUser!.UserID,
                        txtName.Text.Trim(),
                        txtUsername.Text.Trim(),
                        cmbRole.SelectedItem.ToString()!,
                        chkIsActive.Checked
                    );
                }
                else
                {
                    success = await ManagerService.CreateUserAsync(
                        txtName.Text.Trim(),
                        txtUsername.Text.Trim(),
                        txtPassword.Text,
                        cmbRole.SelectedItem.ToString()!
                    );
                }

                if (success)
                {
                    MessageBox.Show($"User {(isEditMode ? "updated" : "created")} successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {(isEditMode ? "updating" : "creating")} user: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = isEditMode ? "Update User" : "Create User";
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (cmbRole.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a role.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbRole.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }

    partial class AddUserForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Label lblRole;
        private ComboBox cmbRole;
        private CheckBox chkIsActive;
        private Button btnSave;
        private Button btnCancel;
        private Panel pnlMain;

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
            this.lblTitle = new Label();
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.lblUsername = new Label();
            this.txtUsername = new TextBox();
            this.lblPassword = new Label();
            this.txtPassword = new TextBox();
            this.lblRole = new Label();
            this.cmbRole = new ComboBox();
            this.chkIsActive = new CheckBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.pnlMain = new Panel();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // pnlMain
            this.pnlMain.BackColor = Color.White;
            this.pnlMain.BorderStyle = BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.lblName);
            this.pnlMain.Controls.Add(this.txtName);
            this.pnlMain.Controls.Add(this.lblUsername);
            this.pnlMain.Controls.Add(this.txtUsername);
            this.pnlMain.Controls.Add(this.lblPassword);
            this.pnlMain.Controls.Add(this.txtPassword);
            this.pnlMain.Controls.Add(this.lblRole);
            this.pnlMain.Controls.Add(this.cmbRole);
            this.pnlMain.Controls.Add(this.chkIsActive);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Location = new Point(20, 20);
            this.pnlMain.Size = new Size(400, 350);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Size = new Size(150, 30);
            this.lblTitle.Text = "Add New User";

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Font = new Font("Segoe UI", 10F);
            this.lblName.Location = new Point(20, 70);
            this.lblName.Size = new Size(42, 19);
            this.lblName.Text = "Name:";

            // txtName
            this.txtName.Font = new Font("Segoe UI", 10F);
            this.txtName.Location = new Point(20, 95);
            this.txtName.Size = new Size(350, 25);
            this.txtName.TabIndex = 0;

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new Font("Segoe UI", 10F);
            this.lblUsername.Location = new Point(20, 130);
            this.lblUsername.Size = new Size(71, 19);
            this.lblUsername.Text = "Username:";

            // txtUsername
            this.txtUsername.Font = new Font("Segoe UI", 10F);
            this.txtUsername.Location = new Point(20, 155);
            this.txtUsername.Size = new Size(350, 25);
            this.txtUsername.TabIndex = 1;

            // lblPassword
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new Font("Segoe UI", 10F);
            this.lblPassword.Location = new Point(20, 190);
            this.lblPassword.Size = new Size(70, 19);
            this.lblPassword.Text = "Password:";

            // txtPassword
            this.txtPassword.Font = new Font("Segoe UI", 10F);
            this.txtPassword.Location = new Point(20, 215);
            this.txtPassword.Size = new Size(350, 25);
            this.txtPassword.TabIndex = 2;
            this.txtPassword.UseSystemPasswordChar = true;

            // lblRole
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new Font("Segoe UI", 10F);
            this.lblRole.Location = new Point(20, 250);
            this.lblRole.Size = new Size(36, 19);
            this.lblRole.Text = "Role:";

            // cmbRole
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Font = new Font("Segoe UI", 10F);
            this.cmbRole.Location = new Point(20, 275);
            this.cmbRole.Size = new Size(200, 25);
            this.cmbRole.TabIndex = 3;

            // chkIsActive
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Checked = true;
            this.chkIsActive.CheckState = CheckState.Checked;
            this.chkIsActive.Font = new Font("Segoe UI", 10F);
            this.chkIsActive.Location = new Point(240, 277);
            this.chkIsActive.Size = new Size(64, 23);
            this.chkIsActive.TabIndex = 4;
            this.chkIsActive.Text = "Active";
            this.chkIsActive.UseVisualStyleBackColor = true;

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(40, 167, 69);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(190, 310);
            this.btnSave.Size = new Size(90, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Create User";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(290, 310);
            this.btnCancel.Size = new Size(80, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // AddUserForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.ClientSize = new Size(440, 390);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUserForm";
            this.Text = "Add User";
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}