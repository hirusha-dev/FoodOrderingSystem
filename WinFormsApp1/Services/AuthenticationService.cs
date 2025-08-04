using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Services/AuthenticationService.cs
using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Services
{
    public class AuthenticationService
    {
        public static User? CurrentUser { get; private set; }

        public static async Task<LoginResult> LoginAsync(string username, string password)
        {
            try
            {
                using var context = DatabaseService.CreateContext();

                var user = await context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

                if (user == null)
                {
                    return new LoginResult { Success = false, Message = "Username not found." };
                }

                // In a real application, you should hash passwords
                // For this demo, we're using plain text (NOT recommended for production)
                if (user.Password != password)
                {
                    return new LoginResult { Success = false, Message = "Invalid password." };
                }

                CurrentUser = user;
                return new LoginResult { Success = true, User = user, Message = "Login successful." };
            }
            catch (Exception ex)
            {
                return new LoginResult { Success = false, Message = $"Login error: {ex.Message}" };
            }
        }

        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

        public static bool HasRole(string role)
        {
            return CurrentUser?.Role == role;
        }

        public static bool IsWaiter()
        {
            return HasRole("Waiter");
        }

        public static bool IsChef()
        {
            return HasRole("Chef");
        }

        public static bool IsManager()
        {
            return HasRole("Manager");
        }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public User? User { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}