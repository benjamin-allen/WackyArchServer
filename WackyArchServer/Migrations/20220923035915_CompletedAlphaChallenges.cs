using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class CompletedAlphaChallenges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompletedChallenge",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlphaChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedAlphaChallenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedAlphaChallenges_AlphaChallenges_AlphaChallengeId",
                        column: x => x.AlphaChallengeId,
                        principalTable: "AlphaChallenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompletedAlphaChallenges_AlphaChallengeId",
                table: "CompletedChallenge",
                column: "AlphaChallengeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedChallenge");
        }
    }
}
