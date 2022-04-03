using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class CardComponent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Cards",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckListComponent",
                columns: table => new
                {
                    CheckListComponentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CardId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListComponent", x => x.CheckListComponentId);
                    table.ForeignKey(
                        name: "FK_CheckListComponent_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CheckListItem",
                columns: table => new
                {
                    CheckListItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: true),
                    IsChecked = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    BelongsToCheckListComponentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckListItem", x => x.CheckListItemId);
                    table.ForeignKey(
                        name: "FK_CheckListItem_CheckListComponent_BelongsToCheckListComponen~",
                        column: x => x.BelongsToCheckListComponentId,
                        principalTable: "CheckListComponent",
                        principalColumn: "CheckListComponentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckListComponent_CardId",
                table: "CheckListComponent",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckListItem_BelongsToCheckListComponentId",
                table: "CheckListItem",
                column: "BelongsToCheckListComponentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckListItem");

            migrationBuilder.DropTable(
                name: "CheckListComponent");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Cards");
        }
    }
}
