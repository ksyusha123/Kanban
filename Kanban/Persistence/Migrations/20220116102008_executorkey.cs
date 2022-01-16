using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class executorkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Executor_ExecutorId",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Executor_AuthorId",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Executor",
                table: "Executor");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Comment",
                newName: "AuthorTelegramUsername");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorId",
                table: "Comment",
                newName: "IX_Comment_AuthorTelegramUsername");

            migrationBuilder.RenameColumn(
                name: "ExecutorId",
                table: "Card",
                newName: "ExecutorTelegramUsername");

            migrationBuilder.RenameIndex(
                name: "IX_Card_ExecutorId",
                table: "Card",
                newName: "IX_Card_ExecutorTelegramUsername");

            migrationBuilder.AlterColumn<string>(
                name: "TelegramUsername",
                table: "Executor",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Executor",
                table: "Executor",
                column: "TelegramUsername");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Executor_ExecutorTelegramUsername",
                table: "Card",
                column: "ExecutorTelegramUsername",
                principalTable: "Executor",
                principalColumn: "TelegramUsername",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Executor_AuthorTelegramUsername",
                table: "Comment",
                column: "AuthorTelegramUsername",
                principalTable: "Executor",
                principalColumn: "TelegramUsername",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Executor_ExecutorTelegramUsername",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Executor_AuthorTelegramUsername",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Executor",
                table: "Executor");

            migrationBuilder.RenameColumn(
                name: "AuthorTelegramUsername",
                table: "Comment",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_AuthorTelegramUsername",
                table: "Comment",
                newName: "IX_Comment_AuthorId");

            migrationBuilder.RenameColumn(
                name: "ExecutorTelegramUsername",
                table: "Card",
                newName: "ExecutorId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_ExecutorTelegramUsername",
                table: "Card",
                newName: "IX_Card_ExecutorId");

            migrationBuilder.AlterColumn<string>(
                name: "TelegramUsername",
                table: "Executor",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Executor",
                table: "Executor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Executor_ExecutorId",
                table: "Card",
                column: "ExecutorId",
                principalTable: "Executor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Executor_AuthorId",
                table: "Comment",
                column: "AuthorId",
                principalTable: "Executor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
