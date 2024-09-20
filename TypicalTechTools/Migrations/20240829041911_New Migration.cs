using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TypicalTechTools.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "Id", "Email", "Password", "UserName" },
                values: new object[] { 1, "Test@email.com", "$2a$11$SB.Ji90.ZIqJh6/qaWhe5OL1L2J3AQYCxl6Xh0PKpHUI.Hw8Xq7.O", "Alex" });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Code", "CreatedDate", "ProductsId", "SessionId", "Text" },
                values: new object[,]
                {
                    { 1, 12345, null, null, null, "This is a great product. Highly Recommended." },
                    { 2, 12350, null, null, null, "Not worth the excessive price. Stick with a cheaper generic one." },
                    { 3, 12345, null, null, null, "A great budget buy. As good as some of the expensive alternatives." },
                    { 4, 12347, null, null, null, "Total garbage. Never buying this brand again." }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "Description", "Name", "Price", "SessionId", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 12345, "bluetooth headphones with fair battery life and a 1 month warranty", "Generic Headphones", 84.989999999999995, null, null },
                    { 2, 12346, "bluetooth headphones with good battery life and a 6 month warranty", "Expensive Headphones", 149.99000000000001, null, null },
                    { 3, 12347, "bluetooth headphones with good battery life and a 12 month warranty", "Name Brand Headphones", 199.99000000000001, null, null },
                    { 4, 12348, "simple bluetooth pointing device", "Generic Wireless Mouse", 39.990000000000002, null, null },
                    { 5, 12349, "mouse and keyboard wired combination", "Logitach Mouse and Keyboard", 73.989999999999995, null, null },
                    { 6, 12350, "quality wireless mouse", "Logitach Wireless Mouse", 149.99000000000001, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductsId",
                table: "Comments",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
