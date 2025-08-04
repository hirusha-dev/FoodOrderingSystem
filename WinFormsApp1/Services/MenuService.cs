using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Services/MenuService.cs
using FoodOrderingSystem.Data;
using FoodOrderingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Services
{
    public static class MenuService
    {
        public static async Task<List<MenuItem>> GetAllMenuItemsAsync()
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.MenuItems
                    .Where(item => item.IsAvailable)
                    .OrderBy(item => item.Category)
                    .ThenBy(item => item.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading menu items: {ex.Message}");
            }
        }

        public static async Task<List<MenuItem>> GetMenuItemsByCategoryAsync(string category)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.MenuItems
                    .Where(item => item.IsAvailable && item.Category == category)
                    .OrderBy(item => item.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading menu items by category: {ex.Message}");
            }
        }

        public static async Task<List<string>> GetAllCategoriesAsync()
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.MenuItems
                    .Where(item => item.IsAvailable)
                    .Select(item => item.Category)
                    .Distinct()
                    .OrderBy(category => category)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading categories: {ex.Message}");
            }
        }

        public static async Task<MenuItem?> GetMenuItemByIdAsync(int itemId)
        {
            try
            {
                using var context = DatabaseService.CreateContext();
                return await context.MenuItems
                    .FirstOrDefaultAsync(item => item.ItemID == itemId && item.IsAvailable);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading menu item: {ex.Message}");
            }
        }
    }
}