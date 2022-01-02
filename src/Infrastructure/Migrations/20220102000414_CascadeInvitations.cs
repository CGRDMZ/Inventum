using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CascadeInvitations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Boards_InvitedToBoardId",
                table: "Invitation");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Boards_InvitedToBoardId",
                table: "Invitation",
                column: "InvitedToBoardId",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitation_Boards_InvitedToBoardId",
                table: "Invitation");

            migrationBuilder.AddForeignKey(
                name: "FK_Invitation_Boards_InvitedToBoardId",
                table: "Invitation",
                column: "InvitedToBoardId",
                principalTable: "Boards",
                principalColumn: "BoardId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
