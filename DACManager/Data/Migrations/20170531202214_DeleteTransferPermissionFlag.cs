using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DACManager.Data.Migrations
{
    public partial class DeleteTransferPermissionFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Transfer",
                table: "Permissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Transfer",
                table: "Permissions",
                nullable: false,
                defaultValue: false);
        }
    }
}
