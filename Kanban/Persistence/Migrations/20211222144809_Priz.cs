using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Priz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Chat",
                newName: "BoardId");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Chat",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "Chat",
                newName: "ProjectId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Chat",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
