using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class MultipleUserAndInvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_OwnerUserId",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_OwnerUserId",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Boards");

            migrationBuilder.CreateTable(
                name: "BoardUser",
                columns: table => new
                {
                    BoardsBoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnersUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardUser", x => new { x.BoardsBoardId, x.OwnersUserId });
                    table.ForeignKey(
                        name: "FK_BoardUser_Boards_BoardsBoardId",
                        column: x => x.BoardsBoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardUser_Users_OwnersUserId",
                        column: x => x.OwnersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitation",
                columns: table => new
                {
                    InvitationId = table.Column<Guid>(type: "uuid", nullable: false),
                    InvitedToBoardId = table.Column<Guid>(type: "uuid", nullable: true),
                    InvitedUserUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitation", x => x.InvitationId);
                    table.ForeignKey(
                        name: "FK_Invitation_Boards_InvitedToBoardId",
                        column: x => x.InvitedToBoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invitation_Users_InvitedUserUserId",
                        column: x => x.InvitedUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_BoardUser_OwnersUserId",
                table: "BoardUser",
                column: "OwnersUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_InvitedToBoardId",
                table: "Invitation",
                column: "InvitedToBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitation_InvitedUserUserId",
                table: "Invitation",
                column: "InvitedUserUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardUser");

            migrationBuilder.DropTable(
                name: "Invitation");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerUserId",
                table: "Boards",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_OwnerUserId",
                table: "Boards",
                column: "OwnerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_OwnerUserId",
                table: "Boards",
                column: "OwnerUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
