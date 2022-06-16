using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MissionSystem.Data.Migrations
{
    public partial class RemoveUnused : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arenas_Gadgets_GadgetMacAddress",
                table: "Arenas");

            migrationBuilder.DropForeignKey(
                name: "FK_Arenas_Matches_GameId",
                table: "Arenas");

            migrationBuilder.DropTable(
                name: "GadgetMatch");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Gadgets_MacAddress",
                table: "Gadgets");

            migrationBuilder.DropIndex(
                name: "IX_Arenas_GadgetMacAddress",
                table: "Arenas");

            migrationBuilder.DropIndex(
                name: "IX_Arenas_GameId",
                table: "Arenas");

            migrationBuilder.DropColumn(
                name: "GadgetMacAddress",
                table: "Arenas");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "Arenas");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GadgetMacAddress",
                table: "Arenas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "Arenas",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    GameTypeName = table.Column<string>(type: "text", nullable: false),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GadgetMatch",
                columns: table => new
                {
                    GadgetsMacAddress = table.Column<string>(type: "text", nullable: false),
                    MatchesId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GadgetMatch", x => new { x.GadgetsMacAddress, x.MatchesId });
                    table.ForeignKey(
                        name: "FK_GadgetMatch_Gadgets_GadgetsMacAddress",
                        column: x => x.GadgetsMacAddress,
                        principalTable: "Gadgets",
                        principalColumn: "MacAddress",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GadgetMatch_Matches_MatchesId",
                        column: x => x.MatchesId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gadgets_MacAddress",
                table: "Gadgets",
                column: "MacAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Arenas_GadgetMacAddress",
                table: "Arenas",
                column: "GadgetMacAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Arenas_GameId",
                table: "Arenas",
                column: "GameId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GadgetMatch_MatchesId",
                table: "GadgetMatch",
                column: "MatchesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arenas_Gadgets_GadgetMacAddress",
                table: "Arenas",
                column: "GadgetMacAddress",
                principalTable: "Gadgets",
                principalColumn: "MacAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_Arenas_Matches_GameId",
                table: "Arenas",
                column: "GameId",
                principalTable: "Matches",
                principalColumn: "Id");
        }
    }
}
