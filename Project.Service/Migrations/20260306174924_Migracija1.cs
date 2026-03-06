using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Project.Service.Migrations
{
    /// <inheritdoc />
    public partial class Migracija1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Elektronicki uredjaji", "Elektronika" },
                    { 2, "Muska i zenska roba", "Odjeća" },
                    { 3, "Prehrambeni proizvodi", "Prehrana" },
                    { 4, "Stvari za kucne ljubimce", "Pet's" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "IsActive", "Name", "Price", "Stock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Laptop", 1200m, 10 },
                    { 2, 1, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Stolno racunalo", 900m, 30 },
                    { 3, 1, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Monitor", 350m, 50 },
                    { 4, 2, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Majica", 25m, 100 },
                    { 5, 2, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Hlace", 30m, 5012 },
                    { 6, 3, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Zelena salata", 0.99m, 10000 },
                    { 7, 3, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Kruh", 1.30m, 535 },
                    { 8, 4, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "Loptica", 2m, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductCategories",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
