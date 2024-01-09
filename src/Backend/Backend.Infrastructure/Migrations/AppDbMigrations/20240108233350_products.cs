using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations.AppDbMigrations
{
    /// <inheritdoc />
    public partial class products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Active", "Created", "CreatedBy", "Description", "LiquidWeight", "Name", "ProductTypeId", "SKU", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("4c16cd4a-1aef-41b9-9c5b-cf9af8421546"), true, null, null, "Produto de teste gerado na migration - SampleCompany", 0m, "Motorola Moto E", 3, "202401", new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0m, null, null, 100m },
                    { new Guid("ee5fa645-68e9-4ffe-bff9-44bc37ca34ef"), true, null, null, "Produto de teste gerado na migration - Aurora", 0m, "Samsung Galaxy S4", 3, "202401", new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0m, null, null, 100m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("4c16cd4a-1aef-41b9-9c5b-cf9af8421546"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("ee5fa645-68e9-4ffe-bff9-44bc37ca34ef"));
        }
    }
}
