using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class RemoveAccountUseClaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RunLogs_Accounts_SubmitterAccountId",
                table: "RunLogs");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_RunLogs_SubmitterAccountId",
                table: "RunLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Passwordhash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RunLogs_SubmitterAccountId",
                table: "RunLogs",
                column: "SubmitterAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_RunLogs_Accounts_SubmitterAccountId",
                table: "RunLogs",
                column: "SubmitterAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
