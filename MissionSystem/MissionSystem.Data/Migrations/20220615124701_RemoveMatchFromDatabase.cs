using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionSystem.Data.Migrations
{
    public partial class RemoveMatchFromDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arenas_Matches_GameId",
                table: "Arenas");

            migrationBuilder.DropForeignKey(
                name: "FK_GadgetMatch_Matches_MatchesId",
                table: "GadgetMatch");

            migrationBuilder.DropIndex(
                name: "IX_Arenas_GameId",
                table: "Arenas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.RenameTable(
                name: "Matches",
                newName: "Match");

            migrationBuilder.AddColumn<int>(
                name: "ArenaId",
                table: "Match",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Match",
                table: "Match",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Match_ArenaId",
                table: "Match",
                column: "ArenaId");

            migrationBuilder.AddForeignKey(
                name: "FK_GadgetMatch_Match_MatchesId",
                table: "GadgetMatch",
                column: "MatchesId",
                principalTable: "Match",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Match_Arenas_ArenaId",
                table: "Match",
                column: "ArenaId",
                principalTable: "Arenas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GadgetMatch_Match_MatchesId",
                table: "GadgetMatch");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_Arenas_ArenaId",
                table: "Match");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Match",
                table: "Match");

            migrationBuilder.DropIndex(
                name: "IX_Match_ArenaId",
                table: "Match");

            migrationBuilder.DropColumn(
                name: "ArenaId",
                table: "Match");

            migrationBuilder.RenameTable(
                name: "Match",
                newName: "Matches");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_GadgetMatch_Matches_MatchesId",
                table: "GadgetMatch",
                column: "MatchesId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
