using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodOrderingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondChefAndWaiter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedDate", "IsActive", "Name", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 4, new DateTime(2025, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), true, "Alice Waiter", "waiter123", "Waiter", "waiter2" },
                    { 5, new DateTime(2025, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), true, "Carlos Chef", "chef123", "Chef", "chef2" }
                });

            migrationBuilder.InsertData(
                table: "Chefs",
                columns: new[] { "UserID", "ShiftTime", "Specialty" },
                values: new object[] { 5, "Night Shift", "Seafood" });

            migrationBuilder.InsertData(
                table: "Waiters",
                columns: new[] { "UserID", "ShiftTime", "Specialty" },
                values: new object[] { 4, "Evening Shift", "Outdoor Tables" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Chefs",
                keyColumn: "UserID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Waiters",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 5);
        }
    }
}
