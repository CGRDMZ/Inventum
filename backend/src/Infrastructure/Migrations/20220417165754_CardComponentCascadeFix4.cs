using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CardComponentCascadeFix4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListComponent_Cards_CardId",
                table: "CheckListComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListComponent_Cards_CardId1",
                table: "CheckListComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListItem_CheckListComponent_CheckListComponentId",
                table: "CheckListItem");

            migrationBuilder.DropIndex(
                name: "IX_CheckListItem_CheckListComponentId",
                table: "CheckListItem");

            migrationBuilder.DropIndex(
                name: "IX_CheckListComponent_CardId",
                table: "CheckListComponent");

            migrationBuilder.DropIndex(
                name: "IX_CheckListComponent_CardId1",
                table: "CheckListComponent");

            migrationBuilder.DropColumn(
                name: "CheckListComponentId",
                table: "CheckListItem");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "CheckListComponent");

            migrationBuilder.DropColumn(
                name: "CardId1",
                table: "CheckListComponent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CheckListComponentId",
                table: "CheckListItem",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardId",
                table: "CheckListComponent",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CardId1",
                table: "CheckListComponent",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItem_CheckListComponentId",
                table: "CheckListItem",
                column: "CheckListComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListComponent_CardId",
                table: "CheckListComponent",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListComponent_CardId1",
                table: "CheckListComponent",
                column: "CardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListComponent_Cards_CardId",
                table: "CheckListComponent",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListComponent_Cards_CardId1",
                table: "CheckListComponent",
                column: "CardId1",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListItem_CheckListComponent_CheckListComponentId",
                table: "CheckListItem",
                column: "CheckListComponentId",
                principalTable: "CheckListComponent",
                principalColumn: "CheckListComponentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
