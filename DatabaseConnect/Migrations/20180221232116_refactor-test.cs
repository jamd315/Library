using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class refactortest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAuthor_tblBook_BookID",
                table: "tblAuthor");

            migrationBuilder.DropIndex(
                name: "IX_tblAuthor_BookID",
                table: "tblAuthor");

            migrationBuilder.DropColumn(
                name: "SpecialCollection",
                table: "tblBook");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "tblAuthor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SpecialCollection",
                table: "tblBook",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "tblAuthor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblAuthor_BookID",
                table: "tblAuthor",
                column: "BookID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAuthor_tblBook_BookID",
                table: "tblAuthor",
                column: "BookID",
                principalTable: "tblBook",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
