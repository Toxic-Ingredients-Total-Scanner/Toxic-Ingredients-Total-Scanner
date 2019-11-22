using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TITS_API.Repositories.Migrations
{
    public partial class Product_Model_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BrandName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ManufacturerName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PubChemID",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLegal",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Products",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChemicalSafety",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GHSClassificationRaportURL",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PubChemCID",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StructureImageURL",
                table: "Ingredients",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsLegal",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ChemicalSafety",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "GHSClassificationRaportURL",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "PubChemCID",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "StructureImageURL",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "BrandName",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturerName",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PubChemID",
                table: "Ingredients",
                type: "integer",
                nullable: true);
        }
    }
}
