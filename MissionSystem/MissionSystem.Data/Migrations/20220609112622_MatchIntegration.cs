using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionSystem.Data.Migrations
{
    public partial class MatchIntegration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Arenas_ArenaId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_ArenaId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_GadgetTypes_RefId",
                table: "GadgetTypes");

            migrationBuilder.DropColumn(
                name: "ArenaId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Arenas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_MacAddress",
                table: "Gadgets",
                column: "MacAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arenas_GameId",
                table: "Arenas",
                column: "GameId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Arenas_Matches_GameId",
                table: "Arenas",
                column: "GameId",
                principalTable: "Matches",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arenas_Matches_GameId",
                table: "Arenas");

            migrationBuilder.DropIndex(
                name: "IX_Gadgets_MacAddress",
                table: "Gadgets");

            migrationBuilder.DropIndex(
                name: "IX_Arenas_GameId",
                table: "Arenas");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Arenas");

            migrationBuilder.AddColumn<int>(
                name: "ArenaId",
                table: "Matches",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ArenaId",
                table: "Matches",
                column: "ArenaId");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetTypes_RefId",
                table: "GadgetTypes",
                column: "RefId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Arenas_ArenaId",
                table: "Matches",
                column: "ArenaId",
                principalTable: "Arenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
