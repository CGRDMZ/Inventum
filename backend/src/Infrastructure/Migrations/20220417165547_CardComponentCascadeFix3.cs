using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CardComponentCascadeFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CardId1",
                table: "CheckListComponent",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckListComponent_CardId1",
                table: "CheckListComponent",
                column: "CardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListComponent_Cards_CardId1",
                table: "CheckListComponent",
                column: "CardId1",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListComponent_Cards_CardId1",
                table: "CheckListComponent");

            migrationBuilder.DropIndex(
                name: "IX_CheckListComponent_CardId1",
                table: "CheckListComponent");

            migrationBuilder.DropColumn(
                name: "CardId1",
                table: "CheckListComponent");
        }
    }
}
