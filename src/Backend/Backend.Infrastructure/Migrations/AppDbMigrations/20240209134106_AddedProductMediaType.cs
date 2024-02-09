using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations.AppDbMigrations
{
    /// <inheritdoc />
    public partial class AddedProductMediaType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("88d1faf4-1056-404d-b4be-f563bb44ed26"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("d63ab158-5c64-4fcf-a12f-5a0c3849b0d5"));

            migrationBuilder.CreateTable(
                name: "ProductMedia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    MediaType = table.Column<int>(type: "integer", nullable: false),
                    MediaURL = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<long>(type: "bigint", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMedia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductMedia_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 2, 9, 13, 41, 5, 899, DateTimeKind.Utc).AddTicks(9130));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Active", "CategoryId", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("30e3de5e-870c-4a80-a673-86ebdf6e9513"), true, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), new DateTime(2024, 2, 9, 13, 41, 5, 899, DateTimeKind.Utc).AddTicks(9339), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 },
                    { new Guid("dc8fdc5d-5774-4d01-b2e0-8cba4145425f"), true, null, new DateTime(2024, 2, 9, 13, 41, 5, 899, DateTimeKind.Utc).AddTicks(9368), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 2, 9, 13, 41, 5, 899, DateTimeKind.Utc).AddTicks(9264));

            migrationBuilder.CreateIndex(
                name: "IX_ProductMedia_ProductId",
                table: "ProductMedia",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductMedia");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("30e3de5e-870c-4a80-a673-86ebdf6e9513"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("dc8fdc5d-5774-4d01-b2e0-8cba4145425f"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 1, 14, 17, 36, 55, 685, DateTimeKind.Utc).AddTicks(162));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Active", "CategoryId", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("88d1faf4-1056-404d-b4be-f563bb44ed26"), true, null, new DateTime(2024, 1, 14, 17, 36, 55, 685, DateTimeKind.Utc).AddTicks(313), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 },
                    { new Guid("d63ab158-5c64-4fcf-a12f-5a0c3849b0d5"), true, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), new DateTime(2024, 1, 14, 17, 36, 55, 685, DateTimeKind.Utc).AddTicks(306), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 1, 14, 17, 36, 55, 685, DateTimeKind.Utc).AddTicks(270));
        }
    }
}
