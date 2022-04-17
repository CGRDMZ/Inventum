using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CardComponentCascadeFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckListComponent_Cards_BelongsToCardId",
                table: "CheckListComponent");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckListComponent_Cards_CardId",
                table: "CheckListComponent");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListComponent_Cards_BelongsToCardId",
                table: "CheckListComponent",
                column: "BelongsToCardId",
                principalTable: "Cards",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckListComponent_Cards_CardId",
                table: "CheckListComponent",
                column: "CardId",
                principalTable: "Cards",
                principalColumn: "CardId",
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
        }
    }
}
