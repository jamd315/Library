using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class prefinalmaybe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBook_tblCovers_CoverID1",
                table: "tblBook");

            migrationBuilder.DropIndex(
                name: "IX_tblBook_CoverID1",
                table: "tblBook");

            migrationBuilder.DropColumn(
                name: "CoverID1",
                table: "tblBook");

            migrationBuilder.CreateIndex(
                name: "IX_tblBook_CoverID",
                table: "tblBook",
                column: "CoverID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBook_tblCovers_CoverID",
                table: "tblBook",
                column: "CoverID",
                principalTable: "tblCovers",
                principalColumn: "CoverID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBook_tblCovers_CoverID",
                table: "tblBook");

            migrationBuilder.DropIndex(
                name: "IX_tblBook_CoverID",
                table: "tblBook");

            migrationBuilder.AddColumn<int>(
                name: "CoverID1",
                table: "tblBook",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblBook_CoverID1",
                table: "tblBook",
                column: "CoverID1");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBook_tblCovers_CoverID1",
                table: "tblBook",
                column: "CoverID1",
                principalTable: "tblCovers",
                principalColumn: "CoverID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
