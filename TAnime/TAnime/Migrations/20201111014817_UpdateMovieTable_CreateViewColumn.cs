using Microsoft.EntityFrameworkCore.Migrations;

namespace TAnime.Migrations
{
    public partial class UpdateMovieTable_CreateViewColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "View",
                table: "Movies",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "View",
                table: "Movies");
        }
    }
}
