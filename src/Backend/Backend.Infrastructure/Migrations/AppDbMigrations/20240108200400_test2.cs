using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations.AppDbMigrations
{
    /// <inheritdoc />
    public partial class test2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "Active", "Description", "Name" },
                values: new object[] { 1, true, "Crafting material", "Feedstock" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductType",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
