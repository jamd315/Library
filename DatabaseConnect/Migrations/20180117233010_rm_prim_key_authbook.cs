using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class rm_prim_key_authbook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_tblAuthorBook_AuthBookID",
                table: "tblAuthorBook");

            migrationBuilder.DropColumn(
                name: "AuthBookID",
                table: "tblAuthorBook");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthBookID",
                table: "tblAuthorBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_tblAuthorBook_AuthBookID",
                table: "tblAuthorBook",
                column: "AuthBookID");
        }
    }
}
