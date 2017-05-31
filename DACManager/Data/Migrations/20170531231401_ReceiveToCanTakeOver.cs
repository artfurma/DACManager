using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DACManager.Data.Migrations
{
    public partial class ReceiveToCanTakeOver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Receive",
                table: "Permissions",
                newName: "CanTakeOver");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanTakeOver",
                table: "Permissions",
                newName: "Receive");
        }
    }
}
