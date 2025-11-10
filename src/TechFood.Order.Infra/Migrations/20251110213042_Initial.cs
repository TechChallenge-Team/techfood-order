using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechFood.Order.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderHistory_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "Amount", "CreatedAt", "CustomerId", "Discount", "IsDeleted", "Number", "Status" },
                values: new object[,]
                {
                    { new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), 39.97m, new DateTime(2025, 5, 13, 22, 2, 36, 0, DateTimeKind.Utc).AddTicks(6053), new Guid("25b58f54-63bc-42da-8cf6-8162097e72c8"), 0m, false, 1, 1 },
                    { new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), 26.98m, new DateTime(2025, 5, 13, 22, 2, 36, 0, DateTimeKind.Utc).AddTicks(6354), new Guid("9887b301-605f-46a6-93db-ac1ce8685723"), 0m, false, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "OrderItem",
                columns: new[] { "Id", "IsDeleted", "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("82e5700b-c33e-40a6-bb68-7279f0509421"), false, new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), new Guid("a62dc225-416a-4e36-ba35-a2bd2bbb80f7"), 1, 21.99m },
                    { new Guid("900f65fe-47ca-4b4b-9a7c-a82c6d9c52cd"), false, new Guid("f2b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), new Guid("86c50c81-c46e-4e79-a591-3b68c75cefda"), 1, 4.99m },
                    { new Guid("b0f1c3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), false, new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), new Guid("55f32e65-c82f-4a10-981c-cdb7b0d2715a"), 2, 9.99m },
                    { new Guid("ea31fb90-4bc3-418f-95fc-56516d5bc634"), false, new Guid("d1b5f3a2-4c8e-4b7c-9f0e-5a2d6f3b8c1e"), new Guid("090d8eb0-f514-4248-8512-cf0d61a262f0"), 1, 19.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_OrderId",
                table: "OrderHistory",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrderItem",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
