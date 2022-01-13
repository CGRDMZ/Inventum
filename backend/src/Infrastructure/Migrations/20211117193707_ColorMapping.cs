using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ColorMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BgColor",
                table: "Boards");

            migrationBuilder.AddColumn<int>(
                name: "Color_Blue",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Color_Green",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Color_Red",
                table: "Cards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BgColor_Blue",
                table: "Boards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BgColor_Green",
                table: "Boards",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BgColor_Red",
                table: "Boards",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color_Blue",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Color_Green",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Color_Red",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "BgColor_Blue",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BgColor_Green",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "BgColor_Red",
                table: "Boards");

            migrationBuilder.AddColumn<int>(
                name: "Color",
                table: "Cards",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BgColor",
                table: "Boards",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
