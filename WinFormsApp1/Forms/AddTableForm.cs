// Forms/AddTableForm.cs
using FoodOrderingSystem.Services;
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Forms
{
    public partial class AddTableForm : Form
    {
        private Table? editingTable;
        private bool isEditMode;

        public AddTableForm(Table? table = null)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            editingTable = table;
            isEditMode = table != null;

            if (isEditMode)
            {
                this.Text = "Edit Table";
                lblTitle.Text = "Edit Table";
                btnSave.Text = "Update Table";
                LoadTableData();
            }
            else
            {
                this.Text = "Add New Table";
                lblTitle.Text = "Add New Table";
                btnSave.Text = "Create Table";
            }

            // Populate status combo box
            cmbStatus.Items.AddRange(new string[] { "Available", "Occupied" });
            cmbStatus.SelectedIndex = 0;
        }

        private void LoadTableData()
        {
            if (editingTable != null)
            {
                nudCapacity.Value = editingTable.Capacity;
                cmbStatus.SelectedItem = editingTable.Status;
                lblTableId.Text = $"Table ID: {editingTable.TableID}";
                lblTableId.Visible = true;
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
                    success = await ManagerService.UpdateTableAsync(
                        editingTable!.TableID,
                        (int)nudCapacity.Value,
                        cmbStatus.SelectedItem.ToString()!
                    );
                }
                else
                {
                    success = await ManagerService.CreateTableAsync((int)nudCapacity.Value);
                }

                if (success)
                {
                    MessageBox.Show($"Table {(isEditMode ? "updated" : "created")} successfully!",
                        "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {(isEditMode ? "updating" : "creating")} table: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnSave.Text = isEditMode ? "Update Table" : "Create Table";
            }
        }

        private bool ValidateInput()
        {
            if (nudCapacity.Value < 1)
            {
                MessageBox.Show("Capacity must be at least 1.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCapacity.Focus();
                return false;
            }

            if (cmbStatus.SelectedIndex < 0)
            {
                MessageBox.Show("Please select a status.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
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

    partial class AddTableForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblTableId;
        private Label lblCapacity;
        private NumericUpDown nudCapacity;
        private Label lblStatus;
        private ComboBox cmbStatus;
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
            this.lblTableId = new Label();
            this.lblCapacity = new Label();
            this.nudCapacity = new NumericUpDown();
            this.lblStatus = new Label();
            this.cmbStatus = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.pnlMain = new Panel();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();

            // pnlMain
            this.pnlMain.BackColor = Color.White;
            this.pnlMain.BorderStyle = BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.lblTableId);
            this.pnlMain.Controls.Add(this.lblCapacity);
            this.pnlMain.Controls.Add(this.nudCapacity);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.cmbStatus);
            this.pnlMain.Controls.Add(this.btnSave);
            this.pnlMain.Controls.Add(this.btnCancel);
            this.pnlMain.Location = new Point(20, 20);
            this.pnlMain.Size = new Size(350, 250);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(51, 122, 183);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Size = new Size(150, 30);
            this.lblTitle.Text = "Add New Table";

            // lblTableId
            this.lblTableId.AutoSize = true;
            this.lblTableId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.lblTableId.ForeColor = Color.FromArgb(108, 117, 125);
            this.lblTableId.Location = new Point(20, 60);
            this.lblTableId.Size = new Size(80, 19);
            this.lblTableId.Text = "Table ID: -";
            this.lblTableId.Visible = false;

            // lblCapacity
            this.lblCapacity.AutoSize = true;
            this.lblCapacity.Font = new Font("Segoe UI", 10F);
            this.lblCapacity.Location = new Point(20, 90);
            this.lblCapacity.Size = new Size(63, 19);
            this.lblCapacity.Text = "Capacity:";

            // nudCapacity
            this.nudCapacity.Font = new Font("Segoe UI", 10F);
            this.nudCapacity.Location = new Point(20, 115);
            this.nudCapacity.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.nudCapacity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCapacity.Size = new Size(120, 25);
            this.nudCapacity.TabIndex = 0;
            this.nudCapacity.Value = new decimal(new int[] { 2, 0, 0, 0 });

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Segoe UI", 10F);
            this.lblStatus.Location = new Point(20, 150);
            this.lblStatus.Size = new Size(47, 19);
            this.lblStatus.Text = "Status:";

            // cmbStatus
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new Font("Segoe UI", 10F);
            this.cmbStatus.Location = new Point(20, 175);
            this.cmbStatus.Size = new Size(150, 25);
            this.cmbStatus.TabIndex = 1;

            // btnSave
            this.btnSave.BackColor = Color.FromArgb(40, 167, 69);
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.Location = new Point(160, 210);
            this.btnSave.Size = new Size(90, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Create Table";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.BackColor = Color.FromArgb(108, 117, 125);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.Font = new Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = Color.White;
            this.btnCancel.Location = new Point(260, 210);
            this.btnCancel.Size = new Size(70, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // AddTableForm
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(248, 249, 250);
            this.ClientSize = new Size(390, 290);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddTableForm";
            this.Text = "Add Table";
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacity)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}