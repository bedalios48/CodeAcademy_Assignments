using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignments09_2.Infrastructure.Migrations
{
    public partial class TrackStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "tracks",
                type: "TEXT",
                nullable: false,
                defaultValue: "Active");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "tracks");
        }
    }
}
