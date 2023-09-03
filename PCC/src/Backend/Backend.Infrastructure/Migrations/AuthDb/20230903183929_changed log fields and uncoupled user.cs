using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class changedlogfieldsanduncoupleduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Tenant_TenantId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_TenantId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tenant");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Tenant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "User",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Tenant",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Tenant",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_TenantId",
                table: "User",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Tenant_TenantId",
                table: "User",
                column: "TenantId",
                principalTable: "Tenant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
