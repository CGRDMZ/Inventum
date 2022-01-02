using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CascadeActivities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Boards_BelongsToBoardId",
                table: "Activity");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Boards_BelongsToBoardId",
                table: "Activity",
                column: "BelongsToBoardId",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Boards_BelongsToBoardId",
                table: "Activity");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Boards_BelongsToBoardId",
                table: "Activity",
                column: "BelongsToBoardId",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
