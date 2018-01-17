using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class rename_tblBookAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_tblAuthor_AuthorID",
                table: "BookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthor_tblBook_BookID",
                table: "BookAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor");

            migrationBuilder.RenameTable(
                name: "BookAuthor",
                newName: "tblBookAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_BookID",
                table: "tblBookAuthor",
                newName: "IX_tblBookAuthor_BookID");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthor_AuthorID",
                table: "tblBookAuthor",
                newName: "IX_tblBookAuthor_AuthorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblBookAuthor",
                table: "tblBookAuthor",
                column: "BookAuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblBookAuthor_tblAuthor_AuthorID",
                table: "tblBookAuthor",
                column: "AuthorID",
                principalTable: "tblAuthor",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblBookAuthor_tblBook_BookID",
                table: "tblBookAuthor",
                column: "BookID",
                principalTable: "tblBook",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBookAuthor_tblAuthor_AuthorID",
                table: "tblBookAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_tblBookAuthor_tblBook_BookID",
                table: "tblBookAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tblBookAuthor",
                table: "tblBookAuthor");

            migrationBuilder.RenameTable(
                name: "tblBookAuthor",
                newName: "BookAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_tblBookAuthor_BookID",
                table: "BookAuthor",
                newName: "IX_BookAuthor_BookID");

            migrationBuilder.RenameIndex(
                name: "IX_tblBookAuthor_AuthorID",
                table: "BookAuthor",
                newName: "IX_BookAuthor_AuthorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                column: "BookAuthorID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_tblAuthor_AuthorID",
                table: "BookAuthor",
                column: "AuthorID",
                principalTable: "tblAuthor",
                principalColumn: "AuthorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthor_tblBook_BookID",
                table: "BookAuthor",
                column: "BookID",
                principalTable: "tblBook",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
