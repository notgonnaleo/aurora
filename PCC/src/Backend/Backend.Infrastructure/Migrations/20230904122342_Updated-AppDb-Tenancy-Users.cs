using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAppDbTenancyUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CEAN",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CEANTax",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "CestId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "NcmId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TaxUnitId",
                table: "Product");

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "ProductType",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ICMSId",
                table: "ProductType",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AgentId",
                table: "Product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ICMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<string>(type: "text", nullable: true),
                    ProductType = table.Column<string>(type: "text", nullable: true),
                    TaxValue = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICMS", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_ICMSId",
                table: "ProductType",
                column: "ICMSId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductType_ICMS_ICMSId",
                table: "ProductType",
                column: "ICMSId",
                principalTable: "ICMS",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductType_ICMS_ICMSId",
                table: "ProductType");

            migrationBuilder.DropTable(
                name: "ICMS");

            migrationBuilder.DropIndex(
                name: "IX_ProductType_ICMSId",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "ICMSId",
                table: "ProductType");

            migrationBuilder.DropColumn(
                name: "AgentId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Product");

            migrationBuilder.AddColumn<string>(
                name: "CEAN",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CEANTax",
                table: "Product",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CestId",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NcmId",
                table: "Product",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TaxUnitId",
                table: "Product",
                type: "integer",
                nullable: true);
        }
    }
}
