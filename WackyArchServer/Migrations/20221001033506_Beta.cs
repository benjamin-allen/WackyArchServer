using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class Beta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BetaChallenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredecessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    InputProgramJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BetaChallenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BetaChallenges_BetaChallenges_PredecessorId",
                        column: x => x.PredecessorId,
                        principalTable: "BetaChallenges",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompletedBetaChallenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BetaChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_BetaChallenges_PredecessorId",
                table: "BetaChallenges",
                column: "PredecessorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedBetaChallenges_BetaChallengeId",
                table: "CompletedBetaChallenges",
                column: "BetaChallengeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompletedBetaChallenges");

            migrationBuilder.DropTable(
                name: "BetaChallenges");
        }
    }
}
