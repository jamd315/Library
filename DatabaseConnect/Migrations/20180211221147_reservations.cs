using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class reservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblReservations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    BookID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    datetime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblReservations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_tblReservations_tblBook_BookID",
                        column: x => x.BookID,
                        principalTable: "tblBook",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblReservations_tblUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "tblUsers",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblReservations_BookID",
                table: "tblReservations",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_tblReservations_UserID",
                table: "tblReservations",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblReservations");
        }
    }
}
