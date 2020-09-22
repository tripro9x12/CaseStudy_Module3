﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TAnime.Migrations
{
    public partial class addEpisodeMovieColumnInEpisodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EpisodeMovie",
                table: "Episodes",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeMovie",
                table: "Episodes");
        }
    }
}
