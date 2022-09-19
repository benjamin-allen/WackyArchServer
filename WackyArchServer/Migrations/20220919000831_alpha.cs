using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class alpha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlphaChallenges",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Flag = table.Column<string>(type: "TEXT", nullable: false),
                    PredecessorID = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    InputTextJson = table.Column<string>(type: "TEXT", nullable: false),
                    OutputTextJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlphaChallenges", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlphaChallenges_AlphaChallenges_PredecessorID",
                        column: x => x.PredecessorID,
                        principalTable: "AlphaChallenges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RunLogs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChallengeID = table.Column<Guid>(type: "TEXT", nullable: false),
                    SubmittedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletedTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SubmitterAccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Result = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunLogs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RunLogs_Accounts_SubmitterAccountId",
                        column: x => x.SubmitterAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlphaChallengeTests",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlphaChallengeID = table.Column<Guid>(type: "TEXT", nullable: false),
                    InputTextJson = table.Column<string>(type: "TEXT", nullable: false),
                    OutputTextJson = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlphaChallengeTests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AlphaChallengeTests_AlphaChallenges_AlphaChallengeID",
                        column: x => x.AlphaChallengeID,
                        principalTable: "AlphaChallenges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlphaChallenges_PredecessorID",
                table: "AlphaChallenges",
                column: "PredecessorID");

            migrationBuilder.CreateIndex(
                name: "IX_AlphaChallengeTests_AlphaChallengeID",
                table: "AlphaChallengeTests",
                column: "AlphaChallengeID");

            migrationBuilder.CreateIndex(
                name: "IX_RunLogs_SubmitterAccountId",
                table: "RunLogs",
                column: "SubmitterAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlphaChallengeTests");

            migrationBuilder.DropTable(
                name: "RunLogs");

            migrationBuilder.DropTable(
                name: "AlphaChallenges");
        }
    }
}
