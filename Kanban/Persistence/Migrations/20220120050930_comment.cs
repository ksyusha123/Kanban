using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardId1",
                table: "Comment",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CardId1",
                table: "Comment",
                column: "CardId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Card_CardId1",
                table: "Comment",
                column: "CardId1",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Card_CardId1",
                table: "Comment");

            migrationBuilder.DropIndex(
                name: "IX_Comment_CardId1",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "CardId1",
                table: "Comment");
        }
    }
}
