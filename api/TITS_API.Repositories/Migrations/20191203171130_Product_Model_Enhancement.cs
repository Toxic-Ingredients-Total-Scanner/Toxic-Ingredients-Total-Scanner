using Microsoft.EntityFrameworkCore.Migrations;

namespace TITS_API.Repositories.Migrations
{
    public partial class Product_Model_Enhancement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Base64Image",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Base64Image",
                table: "Products");
        }
    }
}
