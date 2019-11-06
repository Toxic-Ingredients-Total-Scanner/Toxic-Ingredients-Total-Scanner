using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TITS_API.Repositories.Migrations
{
    public partial class Product_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Gtin = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    BrandName = table.Column<string>(nullable: true),
                    BrandOwner = table.Column<string>(nullable: true),
                    ManufacturerName = table.Column<string>(nullable: true),
                    CountryOfOrigin = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
