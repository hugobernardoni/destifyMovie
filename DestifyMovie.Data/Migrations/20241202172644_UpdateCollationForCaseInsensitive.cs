using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DestifyMovie.Data.Migrations
{
    public partial class UpdateCollationForCaseInsensitive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Alterando o collation da coluna Title na tabela Movies
            migrationBuilder.Sql(
                "ALTER TABLE \"Movies\" ALTER COLUMN \"Title\" TYPE VARCHAR(255) COLLATE \"C\""
            );

            // Alterando o collation da coluna Name na tabela Actors
            migrationBuilder.Sql(
                "ALTER TABLE \"Actors\" ALTER COLUMN \"Name\" TYPE VARCHAR(255) COLLATE \"C\""
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertendo as alterações caso a migration seja revertida
            migrationBuilder.Sql(
                "ALTER TABLE \"Movies\" ALTER COLUMN \"Title\" TYPE VARCHAR(255)"
            );

            migrationBuilder.Sql(
                "ALTER TABLE \"Actors\" ALTER COLUMN \"Name\" TYPE VARCHAR(255)"
            );
        }
    }
}
