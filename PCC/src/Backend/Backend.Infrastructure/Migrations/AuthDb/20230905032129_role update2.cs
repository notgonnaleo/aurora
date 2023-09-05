using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class roleupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "UserRole",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_TenantId",
                table: "UserRole",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Tenant_TenantId",
                table: "UserRole",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Tenant_TenantId",
                table: "UserRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRole_TenantId",
                table: "UserRole");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "UserRole");
        }
    }
}
