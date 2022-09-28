using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class ChangeUserAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendMovieNotifications",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SendTvShowNotifications",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "MoviesCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TvShowsCount",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoviesCount",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TvShowsCount",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "SendMovieNotifications",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SendTvShowNotifications",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
