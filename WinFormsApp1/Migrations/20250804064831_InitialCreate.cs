using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodOrderingSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    TableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.TableID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Chefs",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShiftTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chefs", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Chefs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Waiters",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Specialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShiftTime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Waiters", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Waiters_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableNumber = table.Column<int>(type: "int", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    WaiterID = table.Column<int>(type: "int", nullable: false),
                    ChefID = table.Column<int>(type: "int", nullable: true),
                    TableID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Chefs_ChefID",
                        column: x => x.ChefID,
                        principalTable: "Chefs",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Orders_Tables_TableID",
                        column: x => x.TableID,
                        principalTable: "Tables",
                        principalColumn: "TableID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Waiters_WaiterID",
                        column: x => x.WaiterID,
                        principalTable: "Waiters",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SpecialInstructions = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_ItemID",
                        column: x => x.ItemID,
                        principalTable: "MenuItems",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "ItemID", "Category", "Description", "IsAvailable", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Pizza", "Classic pizza with tomato sauce and mozzarella", true, "Margherita Pizza", 12.99m },
                    { 2, "Pasta", "Creamy pasta with grilled chicken", true, "Chicken Pasta", 14.99m },
                    { 3, "Salad", "Fresh romaine lettuce with caesar dressing", true, "Caesar Salad", 8.99m },
                    { 4, "Main Course", "Fresh salmon with herbs and lemon", true, "Grilled Salmon", 18.99m },
                    { 5, "Dessert", "Rich chocolate cake with frosting", true, "Chocolate Cake", 6.99m }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "TableID", "Capacity", "Status" },
                values: new object[,]
                {
                    { 1, 2, "Available" },
                    { 2, 4, "Available" },
                    { 3, 6, "Available" },
                    { 4, 4, "Available" },
                    { 5, 2, "Available" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreatedDate", "IsActive", "Name", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), true, "Admin User", "admin123", "Manager", "admin" },
                    { 2, new DateTime(2025, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), true, "John Waiter", "waiter123", "Waiter", "waiter1" },
                    { 3, new DateTime(2025, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), true, "Maria Chef", "chef123", "Chef", "chef1" }
                });

            migrationBuilder.InsertData(
                table: "Chefs",
                columns: new[] { "UserID", "ShiftTime", "Specialty" },
                values: new object[] { 3, "Day Shift", "Italian Cuisine" });

            migrationBuilder.InsertData(
                table: "Waiters",
                columns: new[] { "UserID", "ShiftTime", "Specialty" },
                values: new object[] { 2, "Day Shift", "Customer Service" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemID",
                table: "OrderItems",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrderItems",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ChefID",
                table: "Orders",
                column: "ChefID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TableID",
                table: "Orders",
                column: "TableID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WaiterID",
                table: "Orders",
                column: "WaiterID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Chefs");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "Waiters");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
