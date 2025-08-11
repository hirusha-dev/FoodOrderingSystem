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
                Console.WriteLine("Initializing application...");
                await DatabaseService.InitializeDatabaseAsync();
                Console.WriteLine("Database initialization completed.");

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
                        _ => throw new InvalidOperationException($"Unknown user role: {AuthenticationService.CurrentUser?.Role}")
                    };

                    Console.WriteLine($"Starting dashboard for {AuthenticationService.CurrentUser?.Role}: {AuthenticationService.CurrentUser?.Name}");
                    Application.Run(mainForm);
                }
                else
                {
                    Console.WriteLine("Login cancelled or failed.");
                }
            }
            catch (Exception ex)
            {
                var errorMessage = $"Application startup error: {ex.Message}";
                Console.WriteLine(errorMessage);
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                MessageBox.Show(errorMessage, "Application Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Console.WriteLine("Application shutting down.");
        }
    }
}