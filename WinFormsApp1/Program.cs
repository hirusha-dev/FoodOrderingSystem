// Program.cs
using FoodOrderingSystem.Data;
using FoodOrderingSystem.Forms;
using FoodOrderingSystem.Services;

namespace FoodOrderingSystem
{
    internal static class Program
    {
        [STAThread]
        static async Task Main()
        {
            // Configure application
            ApplicationConfiguration.Initialize();

            try
            {
                // Initialize database
                await DatabaseService.InitializeDatabaseAsync();

                // Show login form
                using var loginForm = new LoginForm();

                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Login successful, determine which form to show based on role
                    Form mainForm = AuthenticationService.CurrentUser?.Role switch
                    {
                        "Waiter" => new WaiterDashboard(),
                        "Chef" => new ChefDashboard(),
                        "Manager" => new ManagerDashboard(),
                        _ => throw new InvalidOperationException("Unknown user role")
                    };

                    Application.Run(mainForm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Application startup error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}