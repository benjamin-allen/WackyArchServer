using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WackyArchServer.Migrations
{
    public partial class ChallengeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "AlphaChallenges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AlphaChallenges",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "AlphaChallenges");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AlphaChallenges");
        }
    }
}
