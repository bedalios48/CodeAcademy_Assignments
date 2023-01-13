using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenealogyTree.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreatedByUserIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "People",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "ParentsChildren",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_CreatedByUserId",
                table: "People",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentsChildren_CreatedByUserId",
                table: "ParentsChildren",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParentsChildren_Users_CreatedByUserId",
                table: "ParentsChildren",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Users_CreatedByUserId",
                table: "People",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParentsChildren_Users_CreatedByUserId",
                table: "ParentsChildren");

            migrationBuilder.DropForeignKey(
                name: "FK_People_Users_CreatedByUserId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_CreatedByUserId",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_ParentsChildren_CreatedByUserId",
                table: "ParentsChildren");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "ParentsChildren");
        }
    }
}
