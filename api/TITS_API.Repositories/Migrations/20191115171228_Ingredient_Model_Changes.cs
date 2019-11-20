using Microsoft.EntityFrameworkCore.Migrations;

namespace TITS_API.Repositories.Migrations
{
    public partial class Ingredient_Model_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PubChemCID",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "PubChemID",
                table: "Ingredients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PubChemID",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "PubChemCID",
                table: "Ingredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
