using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class ChangeCardFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Column_ColumnId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_ColumnId",
                table: "Card");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Card_ColumnId",
                table: "Card",
                column: "ColumnId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Column_ColumnId",
                table: "Card",
                column: "ColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
