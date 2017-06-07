using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DACManager.Data.Migrations
{
    public partial class Delete_CanViewPermissions_Flag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanViewPermissions",
                table: "Permissions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanViewPermissions",
                table: "Permissions",
                nullable: false,
                defaultValue: false);
        }
    }
}
