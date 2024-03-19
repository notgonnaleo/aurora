using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations.AppDbMigrations
{
    /// <inheritdoc />
    public partial class adressemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("6123c390-acfc-4630-9dbe-b5107b4d9f5b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("bdd09f7d-41c8-4e45-93fe-5bc7e0567ac7"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 3, 18, 20, 53, 56, 574, DateTimeKind.Utc).AddTicks(1722));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "AgentId", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("552e9a73-d42f-443f-8514-6e2bb3c499ea"), true, null, null, "Azul-Marinho", new DateTime(2024, 3, 18, 20, 53, 56, 574, DateTimeKind.Utc).AddTicks(1875), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 },
                    { new Guid("f80a3ab1-7b61-46c3-92ac-1d786da9a730"), true, null, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 3, 18, 20, 53, 56, 574, DateTimeKind.Utc).AddTicks(1867), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 3, 18, 20, 53, 56, 574, DateTimeKind.Utc).AddTicks(1817));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("552e9a73-d42f-443f-8514-6e2bb3c499ea"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("f80a3ab1-7b61-46c3-92ac-1d786da9a730"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 3, 16, 15, 43, 34, 289, DateTimeKind.Utc).AddTicks(6555));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "AgentId", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("6123c390-acfc-4630-9dbe-b5107b4d9f5b"), true, null, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 3, 16, 15, 43, 34, 289, DateTimeKind.Utc).AddTicks(6721), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 },
                    { new Guid("bdd09f7d-41c8-4e45-93fe-5bc7e0567ac7"), true, null, null, "Azul-Marinho", new DateTime(2024, 3, 16, 15, 43, 34, 289, DateTimeKind.Utc).AddTicks(6729), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 3, 16, 15, 43, 34, 289, DateTimeKind.Utc).AddTicks(6644));
        }
    }
}
