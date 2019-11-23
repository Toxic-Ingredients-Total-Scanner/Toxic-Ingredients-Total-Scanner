using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TITS_API.Repositories.Migrations
{
    public partial class HazardStatement_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChemicalSafety",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "GHSClassificationRaportURL",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "ToxicityGrade",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "StructureImageURL",
                table: "Ingredients",
                newName: "StructureImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "GHSClasificationRaportUrl",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "HazardStatements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: true),
                    DescriptionPolish = table.Column<string>(nullable: true),
                    DescriptionEnglish = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HazardStatements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientHazardStatements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IngredientId = table.Column<int>(nullable: false),
                    HazardStatementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientHazardStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientHazardStatements_HazardStatements_HazardStatement~",
                        column: x => x.HazardStatementId,
                        principalTable: "HazardStatements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientHazardStatements_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientHazardStatements_HazardStatementId",
                table: "IngredientHazardStatements",
                column: "HazardStatementId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientHazardStatements_IngredientId",
                table: "IngredientHazardStatements",
                column: "IngredientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientHazardStatements");

            migrationBuilder.DropTable(
                name: "HazardStatements");

            migrationBuilder.DropColumn(
                name: "GHSClasificationRaportUrl",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "StructureImageUrl",
                table: "Ingredients",
                newName: "StructureImageURL");

            migrationBuilder.AddColumn<string>(
                name: "ChemicalSafety",
                table: "Ingredients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GHSClassificationRaportURL",
                table: "Ingredients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToxicityGrade",
                table: "Ingredients",
                type: "text",
                nullable: true);
        }
    }
}
