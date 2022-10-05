using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class RemovePredecessorFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorId",
                table: "AlphaChallenges");

            migrationBuilder.DropForeignKey(
                name: "FK_BetaChallenges_BetaChallenges_PredecessorId",
                table: "BetaChallenges");

            migrationBuilder.DropTable(
                name: "CompletedBetaChallenges");

            migrationBuilder.DropTable(
                name: "CompletedAlphaChallenges");

            migrationBuilder.DropIndex(
                name: "IX_BetaChallenges_PredecessorId",
                table: "BetaChallenges");

            migrationBuilder.DropIndex(
                name: "IX_AlphaChallenges_PredecessorId",
                table: "AlphaChallenges");

            migrationBuilder.CreateTable(
                name: "CompletedChallenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedChallenges", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedChallenges");

            migrationBuilder.CreateTable(
                name: "CompletedBetaChallenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BetaChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedBetaChallenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedBetaChallenges_BetaChallenges_BetaChallengeId",
                        column: x => x.BetaChallengeId,
                        principalTable: "BetaChallenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedAlphaChallenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlphaChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedChallenge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedChallenge_AlphaChallenges_AlphaChallengeId",
                        column: x => x.AlphaChallengeId,
                        principalTable: "AlphaChallenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BetaChallenges_PredecessorId",
                table: "BetaChallenges",
                column: "PredecessorId");

            migrationBuilder.CreateIndex(
                name: "IX_AlphaChallenges_PredecessorId",
                table: "AlphaChallenges",
                column: "PredecessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedBetaChallenges_BetaChallengeId",
                table: "CompletedBetaChallenges",
                column: "BetaChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedChallenge_AlphaChallengeId",
                table: "CompletedChallenge",
                column: "AlphaChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorId",
                table: "AlphaChallenges",
                column: "PredecessorId",
                principalTable: "AlphaChallenges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BetaChallenges_BetaChallenges_PredecessorId",
                table: "BetaChallenges",
                column: "PredecessorId",
                principalTable: "BetaChallenges",
                principalColumn: "Id");
        }
    }
}
