using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CardComponentCascadeFix : Migration
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
                name: "FK_CheckListItem_CheckListComponent_BelongsToCheckListComponen~",
                table: "CheckListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListItem_CheckListComponent_CheckListComponentId",
                table: "CheckListItem");

            migrationBuilder.RenameColumn(
                name: "CardId1",
                table: "CheckListComponent",
                newName: "BelongsToCardId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckListComponent_CardId1",
                table: "CheckListComponent",
                newName: "IX_CheckListComponent_BelongsToCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListComponent_Cards_BelongsToCardId",
                table: "CheckListComponent",
                column: "BelongsToCardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListComponent_Cards_CardId",
                table: "CheckListComponent",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListItem_CheckListComponent_BelongsToCheckListComponen~",
                table: "CheckListItem",
                column: "BelongsToCheckListComponentId",
                principalTable: "CheckListComponent",
                principalColumn: "CheckListComponentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListItem_CheckListComponent_CheckListComponentId",
                table: "CheckListItem",
                column: "CheckListComponentId",
                principalTable: "CheckListComponent",
                principalColumn: "CheckListComponentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListComponent_Cards_BelongsToCardId",
                table: "CheckListComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListComponent_Cards_CardId",
                table: "CheckListComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListItem_CheckListComponent_BelongsToCheckListComponen~",
                table: "CheckListItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListItem_CheckListComponent_CheckListComponentId",
                table: "CheckListItem");

            migrationBuilder.RenameColumn(
                name: "BelongsToCardId",
                table: "CheckListComponent",
                newName: "CardId1");

            migrationBuilder.RenameIndex(
                name: "IX_CheckListComponent_BelongsToCardId",
                table: "CheckListComponent",
                newName: "IX_CheckListComponent_CardId1");

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
                name: "FK_CheckListItem_CheckListComponent_BelongsToCheckListComponen~",
                table: "CheckListItem",
                column: "BelongsToCheckListComponentId",
                principalTable: "CheckListComponent",
                principalColumn: "CheckListComponentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListItem_CheckListComponent_CheckListComponentId",
                table: "CheckListItem",
                column: "CheckListComponentId",
                principalTable: "CheckListComponent",
                principalColumn: "CheckListComponentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
