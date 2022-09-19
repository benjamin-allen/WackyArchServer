using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class UpdateKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorID",
                table: "AlphaChallenges");

            migrationBuilder.DropForeignKey(
                name: "FK_AlphaChallengeTests_AlphaChallenges_AlphaChallengeID",
                table: "AlphaChallengeTests");

            migrationBuilder.RenameColumn(
                name: "ChallengeID",
                table: "RunLogs",
                newName: "ChallengeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "RunLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AlphaChallengeID",
                table: "AlphaChallengeTests",
                newName: "AlphaChallengeId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AlphaChallengeTests",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_AlphaChallengeTests_AlphaChallengeID",
                table: "AlphaChallengeTests",
                newName: "IX_AlphaChallengeTests_AlphaChallengeId");

            migrationBuilder.RenameColumn(
                name: "PredecessorID",
                table: "AlphaChallenges",
                newName: "PredecessorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "AlphaChallenges",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_AlphaChallenges_PredecessorID",
                table: "AlphaChallenges",
                newName: "IX_AlphaChallenges_PredecessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorId",
                table: "AlphaChallenges",
                column: "PredecessorId",
                principalTable: "AlphaChallenges",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlphaChallengeTests_AlphaChallenges_AlphaChallengeId",
                table: "AlphaChallengeTests",
                column: "AlphaChallengeId",
                principalTable: "AlphaChallenges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorId",
                table: "AlphaChallenges");

            migrationBuilder.DropForeignKey(
                name: "FK_AlphaChallengeTests_AlphaChallenges_AlphaChallengeId",
                table: "AlphaChallengeTests");

            migrationBuilder.RenameColumn(
                name: "ChallengeId",
                table: "RunLogs",
                newName: "ChallengeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RunLogs",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "AlphaChallengeId",
                table: "AlphaChallengeTests",
                newName: "AlphaChallengeID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AlphaChallengeTests",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_AlphaChallengeTests_AlphaChallengeId",
                table: "AlphaChallengeTests",
                newName: "IX_AlphaChallengeTests_AlphaChallengeID");

            migrationBuilder.RenameColumn(
                name: "PredecessorId",
                table: "AlphaChallenges",
                newName: "PredecessorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AlphaChallenges",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_AlphaChallenges_PredecessorId",
                table: "AlphaChallenges",
                newName: "IX_AlphaChallenges_PredecessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_AlphaChallenges_AlphaChallenges_PredecessorID",
                table: "AlphaChallenges",
                column: "PredecessorID",
                principalTable: "AlphaChallenges",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AlphaChallengeTests_AlphaChallenges_AlphaChallengeID",
                table: "AlphaChallengeTests",
                column: "AlphaChallengeID",
                principalTable: "AlphaChallenges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
