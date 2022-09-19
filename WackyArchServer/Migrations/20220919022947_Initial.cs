using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passwordhash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlphaChallenges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PredecessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputTextJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputTextJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlphaChallenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlphaChallenges_AlphaChallenges_PredecessorId",
                        column: x => x.PredecessorId,
                        principalTable: "AlphaChallenges",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RunLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    ChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubmittedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmitterAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunLogs", x => x.Id);
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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "newid()"),
                    AlphaChallengeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InputTextJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutputTextJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlphaChallengeTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlphaChallengeTests_AlphaChallenges_AlphaChallengeId",
                        column: x => x.AlphaChallengeId,
                        principalTable: "AlphaChallenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlphaChallenges_PredecessorId",
                table: "AlphaChallenges",
                column: "PredecessorId");

            migrationBuilder.CreateIndex(
                name: "IX_AlphaChallengeTests_AlphaChallengeId",
                table: "AlphaChallengeTests",
                column: "AlphaChallengeId");

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

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
