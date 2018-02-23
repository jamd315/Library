using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class databasefixedmaybe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAuthor_tblBook_BookID",
                table: "tblAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_tblCovers_tblBook_BookID",
                table: "tblCovers");

            migrationBuilder.DropIndex(
                name: "IX_tblCovers_BookID",
                table: "tblCovers");

            migrationBuilder.DropIndex(
                name: "IX_tblAuthor_BookID",
                table: "tblAuthor");

            migrationBuilder.DropColumn(
                name: "SpecialCollection",
                table: "tblBook");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "tblAuthor");

            migrationBuilder.AlterColumn<string>(
                name: "FicID",
                table: "tblBook",
                nullable: true,
                defaultValue: "n/a",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeweyDecimal",
                table: "tblBook",
                nullable: true,
                defaultValue: "n/a",
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoverID",
                table: "tblBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "tblAuthorBook",
                nullable: false,
                defaultValue: 3,
                oldClrType: typeof(int));

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

            migrationBuilder.DropColumn(
                name: "CoverID",
                table: "tblBook");

            migrationBuilder.AlterColumn<string>(
                name: "FicID",
                table: "tblBook",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "n/a");

            migrationBuilder.AlterColumn<string>(
                name: "DeweyDecimal",
                table: "tblBook",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "n/a");

            migrationBuilder.AddColumn<string>(
                name: "SpecialCollection",
                table: "tblBook",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorID",
                table: "tblAuthorBook",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "tblAuthor",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCovers_BookID",
                table: "tblCovers",
                column: "BookID",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_tblCovers_tblBook_BookID",
                table: "tblCovers",
                column: "BookID",
                principalTable: "tblBook",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
