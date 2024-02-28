using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Infrastructure.Migrations.AppDbMigrations
{
    /// <inheritdoc />
    public partial class APPSNAPSHOTV6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("858c6ec5-ffd7-4902-b1cb-d8c7608ec2ea"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("cc3b9314-7538-4fd9-912c-00801e1ae3be"));

            migrationBuilder.InsertData(
                table: "AgentType",
                columns: new[] { "AgentTypeId", "AgentTypeName" },
                values: new object[,]
                {
                    { 1, "Company" },
                    { 2, "Customer" },
                    { 3, "Employee" },
                    { 4, "Vendor" }
                });

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 2, 28, 4, 16, 21, 815, DateTimeKind.Utc).AddTicks(709));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Active", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("a0fb657a-0efc-4bf8-9234-7705bb6a92d1"), true, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 2, 28, 4, 16, 21, 815, DateTimeKind.Utc).AddTicks(851), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 },
                    { new Guid("d3b23481-9e24-42e7-b08f-4c68ae9a9bc3"), true, null, "Azul-Marinho", new DateTime(2024, 2, 28, 4, 16, 21, 815, DateTimeKind.Utc).AddTicks(858), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 2, 28, 4, 16, 21, 815, DateTimeKind.Utc).AddTicks(811));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AgentType",
                keyColumn: "AgentTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AgentType",
                keyColumn: "AgentTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AgentType",
                keyColumn: "AgentTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AgentType",
                keyColumn: "AgentTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a0fb657a-0efc-4bf8-9234-7705bb6a92d1"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("d3b23481-9e24-42e7-b08f-4c68ae9a9bc3"));

            migrationBuilder.UpdateData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"),
                column: "Created",
                value: new DateTime(2024, 2, 28, 3, 29, 21, 991, DateTimeKind.Utc).AddTicks(6252));

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Active", "CategoryId", "ColorName", "Created", "CreatedBy", "Description", "GTIN", "LiquidWeight", "MetricUnitName", "Name", "ProductTypeId", "SKU", "SubCategoryId", "TenantId", "TotalWeight", "Updated", "UpdatedBy", "Value" },
                values: new object[,]
                {
                    { new Guid("858c6ec5-ffd7-4902-b1cb-d8c7608ec2ea"), true, new Guid("63cf51c6-e90e-4725-b6c3-1c40986d6847"), "Preto", new DateTime(2024, 2, 28, 3, 29, 21, 991, DateTimeKind.Utc).AddTicks(6415), null, "Produto de teste gerado na migration - Aurora", "012345678910111213", 0.13, "G", "Samsung Galaxy S4", 3, "202401", new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"), new Guid("cabaa57a-37ff-4871-be7d-0187ed3534a5"), 0.13, null, null, 604.99000000000001 },
                    { new Guid("cc3b9314-7538-4fd9-912c-00801e1ae3be"), true, null, "Azul-Marinho", new DateTime(2024, 2, 28, 3, 29, 21, 991, DateTimeKind.Utc).AddTicks(6422), null, "Produto de teste gerado na migration - SampleCompany", "012345678910111213", 0.0, "G", "Motorola Moto E", 3, "202401", null, new Guid("ae100414-8fbb-4286-839a-5bafc51a84fb"), 0.0, null, null, 100.0 }
                });

            migrationBuilder.UpdateData(
                table: "SubCategory",
                keyColumn: "SubCategoryId",
                keyValue: new Guid("cb1dd75f-6cf2-4c6e-b050-ee80444ad1c6"),
                column: "Created",
                value: new DateTime(2024, 2, 28, 3, 29, 21, 991, DateTimeKind.Utc).AddTicks(6374));
        }
    }
}
