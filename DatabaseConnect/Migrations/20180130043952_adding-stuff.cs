using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class addingstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.AddColumn<string>(
                name: "ISBN",
                table: "tblBook",
                nullable: true);
                */
            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "tblAuthor",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblUType",
                columns: table => new
                {
                    UTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CheckoutLimit = table.Column<int>(nullable: false),
                    UTypeName = table.Column<string>(nullable: true),
                    WriteAccess = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUType", x => x.UTypeID);
                });

            migrationBuilder.CreateTable(
                name: "tblUserUType",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    UTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserUType", x => new { x.UserID, x.UTypeID });
                    table.ForeignKey(
                        name: "FK_tblUserUType_tblUType_UTypeID",
                        column: x => x.UTypeID,
                        principalTable: "tblUType",
                        principalColumn: "UTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblUserUType_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAuthor_BookID",
                table: "tblAuthor",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserUType_UTypeID",
                table: "tblUserUType",
                column: "UTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblAuthor_tblBook_BookID",
                table: "tblAuthor",
                column: "BookID",
                principalTable: "tblBook",
                principalColumn: "BookID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblAuthor_tblBook_BookID",
                table: "tblAuthor");

            migrationBuilder.DropTable(
                name: "tblUserUType");

            migrationBuilder.DropTable(
                name: "tblUType");

            migrationBuilder.DropIndex(
                name: "IX_tblAuthor_BookID",
                table: "tblAuthor");
            /*
            migrationBuilder.DropColumn(
                name: "ISBN",
                table: "tblBook");
                */
            migrationBuilder.DropColumn(
                name: "BookID",
                table: "tblAuthor");
        }
    }
}
