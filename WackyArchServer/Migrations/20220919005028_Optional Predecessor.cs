using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class OptionalPredecessor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorID",
                table: "AlphaChallenges");

            migrationBuilder.AlterColumn<Guid>(
                name: "PredecessorID",
                table: "AlphaChallenges",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorID",
                table: "AlphaChallenges",
                column: "PredecessorID",
                principalTable: "AlphaChallenges",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorID",
                table: "AlphaChallenges");

            migrationBuilder.AlterColumn<Guid>(
                name: "PredecessorID",
                table: "AlphaChallenges",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorID",
                table: "AlphaChallenges",
                column: "PredecessorID",
                principalTable: "AlphaChallenges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
