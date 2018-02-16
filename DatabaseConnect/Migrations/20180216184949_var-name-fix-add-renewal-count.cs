using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class varnamefixaddrenewalcount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "datetime",
                table: "tblReservations",
                newName: "Datetime");

            migrationBuilder.AddColumn<int>(
                name: "Renewals",
                table: "tblCheckouts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Renewals",
                table: "tblCheckouts");

            migrationBuilder.RenameColumn(
                name: "Datetime",
                table: "tblReservations",
                newName: "datetime");
        }
    }
}
