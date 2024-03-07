using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations.AppDbMigrations
{
    /// <inheritdoc />
    public partial class novocampostock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Agent_AgentId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_ProductVariants_VariantId",
                table: "Stock");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Product_ProductId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_AgentId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_ProductId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_VariantId",
                table: "Stock");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("297abe6a-e843-4dd6-8fe3-cf3769436b14"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("b72f87ed-8854-45a3-be99-c8d7b0d4eac8"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Stock",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 3, 7, 22, 27, 18, 617, DateTimeKind.Utc).AddTicks(9558));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "AgentId", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("c1258e0d-8c6e-4672-96d4-c1990d0546a6"), true, null, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 3, 7, 22, 27, 18, 617, DateTimeKind.Utc).AddTicks(9698), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 },
                    { new Guid("d2bf4203-53c8-448b-bf8a-647d84d7c409"), true, null, null, "Azul-Marinho", new DateTime(2024, 3, 7, 22, 27, 18, 617, DateTimeKind.Utc).AddTicks(9704), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 3, 7, 22, 27, 18, 617, DateTimeKind.Utc).AddTicks(9646));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("c1258e0d-8c6e-4672-96d4-c1990d0546a6"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("d2bf4203-53c8-448b-bf8a-647d84d7c409"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Stock");

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 3, 5, 16, 5, 56, 653, DateTimeKind.Utc).AddTicks(8211));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "AgentId", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("297abe6a-e843-4dd6-8fe3-cf3769436b14"), true, null, null, "Azul-Marinho", new DateTime(2024, 3, 5, 16, 5, 56, 653, DateTimeKind.Utc).AddTicks(8358), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 },
                    { new Guid("b72f87ed-8854-45a3-be99-c8d7b0d4eac8"), true, null, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 3, 5, 16, 5, 56, 653, DateTimeKind.Utc).AddTicks(8351), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 3, 5, 16, 5, 56, 653, DateTimeKind.Utc).AddTicks(8285));

            migrationBuilder.CreateIndex(
                name: "IX_Stock_AgentId",
                table: "Stock",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_ProductId",
                table: "Stock",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stock_VariantId",
                table: "Stock",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Agent_AgentId",
                table: "Stock",
                column: "AgentId",
                principalTable: "Agent",
                principalColumn: "AgentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_ProductVariants_VariantId",
                table: "Stock",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "VariantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Product_ProductId",
                table: "Stock",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
