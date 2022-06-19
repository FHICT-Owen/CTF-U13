using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MissionSystem.Data.Migrations
{
    public partial class matches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArenaGadget");

            migrationBuilder.AddColumn<string>(
                name: "GadgetMacAddress",
                table: "Arenas",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    IsEnglish = table.Column<bool>(type: "boolean", nullable: false),
                    ArenaId = table.Column<int>(type: "integer", nullable: false),
                    GameTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Arenas_ArenaId",
                        column: x => x.ArenaId,
                        principalTable: "Arenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Arenas_GadgetMacAddress",
                table: "Arenas",
                column: "GadgetMacAddress");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetMatch_MatchesId",
                table: "GadgetMatch",
                column: "MatchesId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ArenaId",
                table: "Matches",
                column: "ArenaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arenas_Gadgets_GadgetMacAddress",
                table: "Arenas",
                column: "GadgetMacAddress",
                principalTable: "Gadgets",
                principalColumn: "MacAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arenas_Gadgets_GadgetMacAddress",
                table: "Arenas");

            migrationBuilder.DropTable(
                name: "GadgetMatch");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Arenas_GadgetMacAddress",
                table: "Arenas");

            migrationBuilder.DropColumn(
                name: "GadgetMacAddress",
                table: "Arenas");

            migrationBuilder.CreateTable(
                name: "ArenaGadget",
                columns: table => new
                {
                    ArenasId = table.Column<int>(type: "integer", nullable: false),
                    GadgetsMacAddress = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArenaGadget", x => new { x.ArenasId, x.GadgetsMacAddress });
                    table.ForeignKey(
                        name: "FK_ArenaGadget_Arenas_ArenasId",
                        column: x => x.ArenasId,
                        principalTable: "Arenas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArenaGadget_Gadgets_GadgetsMacAddress",
                        column: x => x.GadgetsMacAddress,
                        principalTable: "Gadgets",
                        principalColumn: "MacAddress",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArenaGadget_GadgetsMacAddress",
                table: "ArenaGadget",
                column: "GadgetsMacAddress");
        }
    }
}
