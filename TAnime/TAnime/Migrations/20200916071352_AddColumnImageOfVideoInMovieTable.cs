using Microsoft.EntityFrameworkCore.Migrations;

namespace TAnime.Migrations
{
    public partial class AddColumnImageOfVideoInMovieTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageOfVideo",
                table: "Movies",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageOfVideo",
                table: "Movies");
        }
    }
}
