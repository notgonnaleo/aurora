using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations.AppDbMigration
{
    /// <inheritdoc />
    public partial class paymentchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("7d4e6b9f-4522-4ed2-9f6c-c37d7f6716fb"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("c90fc99b-719b-4f42-9b2a-5464f63b0e34"));

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Payments",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 4, 21, 18, 29, 17, 202, DateTimeKind.Utc).AddTicks(8942));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("4d7172fe-35f3-4a34-a3d6-4bf24c35ef61"), true, null, "Azul-Marinho", new DateTime(2024, 4, 21, 18, 29, 17, 202, DateTimeKind.Utc).AddTicks(9092), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 },
                    { new Guid("897adb05-04b3-40c7-8f85-4f3272f34ecf"), true, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 4, 21, 18, 29, 17, 202, DateTimeKind.Utc).AddTicks(9086), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 4, 21, 18, 29, 17, 202, DateTimeKind.Utc).AddTicks(9048));

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Orders_OrderId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_OrderId",
                table: "Payments");

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("4d7172fe-35f3-4a34-a3d6-4bf24c35ef61"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "ProductId",
                keyValue: new Guid("897adb05-04b3-40c7-8f85-4f3272f34ecf"));

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Payments");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 4, 21, 17, 50, 20, 650, DateTimeKind.Utc).AddTicks(9625));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "ProductId", "Active", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("7d4e6b9f-4522-4ed2-9f6c-c37d7f6716fb"), true, null, "Azul-Marinho", new DateTime(2024, 4, 21, 17, 50, 20, 650, DateTimeKind.Utc).AddTicks(9841), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 },
                    { new Guid("c90fc99b-719b-4f42-9b2a-5464f63b0e34"), true, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 4, 21, 17, 50, 20, 650, DateTimeKind.Utc).AddTicks(9832), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 4, 21, 17, 50, 20, 650, DateTimeKind.Utc).AddTicks(9726));
        }
    }
}
