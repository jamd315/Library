using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DatabaseConnect.Migrations
{
    public partial class addcheckoutskey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblCheckouts",
                table: "tblCheckouts");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "tblCheckouts",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblCheckouts",
                table: "tblCheckouts",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_tblCheckouts_UserID",
                table: "tblCheckouts",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tblCheckouts",
                table: "tblCheckouts");

            migrationBuilder.DropIndex(
                name: "IX_tblCheckouts_UserID",
                table: "tblCheckouts");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "tblCheckouts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tblCheckouts",
                table: "tblCheckouts",
                columns: new[] { "UserID", "BookID" });
        }
    }
}
