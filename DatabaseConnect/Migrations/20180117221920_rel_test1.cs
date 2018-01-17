using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class rel_test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblBookAuthor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblBookAuthor",
                columns: table => new
                {
                    BookAuthorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuthorID = table.Column<int>(nullable: false),
                    BookID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBookAuthor", x => x.BookAuthorID);
                    table.ForeignKey(
                        name: "FK_tblBookAuthor_tblAuthor_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "tblAuthor",
                        principalColumn: "AuthorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblBookAuthor_tblBook_BookID",
                        column: x => x.BookID,
                        principalTable: "tblBook",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblBookAuthor_AuthorID",
                table: "tblBookAuthor",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_tblBookAuthor_BookID",
                table: "tblBookAuthor",
                column: "BookID");
        }
    }
}
