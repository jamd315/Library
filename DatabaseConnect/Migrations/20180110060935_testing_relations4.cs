using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class testing_relations4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblBookAuthor",
                table: "tblBookAuthor");

            migrationBuilder.RenameTable(
                name: "tblBookAuthor",
                newName: "BookAuthor");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "BookAuthor",
                newName: "BookAuthorID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tblBook",
                newName: "BookID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "tblAuthor",
                newName: "AuthorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthor",
                table: "BookAuthor",
                column: "BookAuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorID",
                table: "BookAuthor",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_BookID",
                table: "BookAuthor",
                column: "BookID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_AuthorID",
                table: "BookAuthor");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthor_BookID",
                table: "BookAuthor");

            migrationBuilder.RenameTable(
                name: "BookAuthor",
                newName: "tblBookAuthor");

            migrationBuilder.RenameColumn(
                name: "BookID",
                table: "tblBook",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "tblAuthor",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "BookAuthorID",
                table: "tblBookAuthor",
                newName: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblBookAuthor",
                table: "tblBookAuthor",
                column: "ID");
        }
    }
}
