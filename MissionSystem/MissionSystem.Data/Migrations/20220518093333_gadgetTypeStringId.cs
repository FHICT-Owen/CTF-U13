using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MissionSystem.Data.Migrations
{
    public partial class gadgetTypeStringId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "GadgetTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE \"GadgetTypes\" SET \"RefId\" = LOWER(REPLACE(\"Name\", ' ', '_'));");

            migrationBuilder.CreateIndex(
                name: "IX_GadgetTypes_RefId",
                table: "GadgetTypes",
                column: "RefId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GadgetTypes_RefId",
                table: "GadgetTypes");

            migrationBuilder.DropColumn(
                name: "RefId",
                table: "GadgetTypes");
        }
    }
}
