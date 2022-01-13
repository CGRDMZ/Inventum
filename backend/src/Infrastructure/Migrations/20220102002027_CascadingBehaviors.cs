using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CascadingBehaviors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardGroups_Boards_BoardId",
                table: "CardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardGroups_BelongsToCardGroupId",
                table: "Cards");

            migrationBuilder.AddForeignKey(
                name: "FK_CardGroups_Boards_BoardId",
                table: "CardGroups",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardGroups_BelongsToCardGroupId",
                table: "Cards",
                column: "BelongsToCardGroupId",
                principalTable: "CardGroups",
                principalColumn: "CardGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardGroups_Boards_BoardId",
                table: "CardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_CardGroups_BelongsToCardGroupId",
                table: "Cards");

            migrationBuilder.AddForeignKey(
                name: "FK_CardGroups_Boards_BoardId",
                table: "CardGroups",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_CardGroups_BelongsToCardGroupId",
                table: "Cards",
                column: "BelongsToCardGroupId",
                principalTable: "CardGroups",
                principalColumn: "CardGroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
