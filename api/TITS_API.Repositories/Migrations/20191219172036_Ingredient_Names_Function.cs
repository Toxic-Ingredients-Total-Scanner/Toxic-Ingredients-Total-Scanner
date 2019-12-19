using Microsoft.EntityFrameworkCore.Migrations;

namespace TITS_API.Repositories.Migrations
{
    public partial class Ingredient_Names_Function : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE OR REPLACE FUNCTION public.ingredient_names()
    RETURNS SETOF text 
    LANGUAGE 'sql'
AS 
$$
    Select ""PolishName""
    From public.""Ingredients""
$$;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP FUNCTION public.ingredient_names()
");
        }
    }
}
