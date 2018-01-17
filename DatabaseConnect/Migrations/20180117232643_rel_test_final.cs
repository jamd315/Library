using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class rel_test_final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAuthorBook",
                columns: table => new
                {
                    BookID = table.Column<int>(nullable: false),
                    AuthorID = table.Column<int>(nullable: false),
                    AuthBookID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAuthorBook", x => new { x.BookID, x.AuthorID });
                    table.UniqueConstraint("AK_tblAuthorBook_AuthBookID", x => x.AuthBookID);
                    table.ForeignKey(
                        name: "FK_tblAuthorBook_tblAuthor_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "tblAuthor",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblAuthorBook_tblBook_BookID",
                        column: x => x.BookID,
                        principalTable: "tblBook",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAuthorBook_AuthorID",
                table: "tblAuthorBook",
                column: "AuthorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAuthorBook");
        }
    }
}
