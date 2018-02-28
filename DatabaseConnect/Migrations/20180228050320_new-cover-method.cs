using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class newcovermethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblBook_tblCovers_CoverID",
                table: "tblBook");

            migrationBuilder.DropTable(
                name: "tblCovers");

            migrationBuilder.DropIndex(
                name: "IX_tblBook_CoverID",
                table: "tblBook");

            migrationBuilder.DropColumn(
                name: "CoverID",
                table: "tblBook");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "tblBook",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "tblBook");

            migrationBuilder.AddColumn<int>(
                name: "CoverID",
                table: "tblBook",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tblCovers",
                columns: table => new
                {
                    CoverID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Base64Encoded = table.Column<string>(nullable: true),
                    BookID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCovers", x => x.CoverID);
                });

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
    }
}
