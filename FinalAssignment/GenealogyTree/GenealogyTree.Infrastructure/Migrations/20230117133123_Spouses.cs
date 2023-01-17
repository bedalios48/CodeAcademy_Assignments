using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GenealogyTree.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Spouses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    SpouseId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MarriedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DivorcedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AreDivorced = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spouses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Spouses_People_SpouseId",
                        column: x => x.SpouseId,
                        principalTable: "People",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Spouses_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_CreatedByUserId",
                table: "Spouses",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_PersonId",
                table: "Spouses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_SpouseId",
                table: "Spouses",
                column: "SpouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spouses");
        }
    }
}
