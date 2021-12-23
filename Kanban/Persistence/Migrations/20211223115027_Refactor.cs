using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Refactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_State_StateId",
                table: "Card");

            migrationBuilder.DropTable(
                name: "ExecutorsWithRights");

            migrationBuilder.DropTable(
                name: "StateState");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Chat",
                newName: "BoardId");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Card",
                newName: "ColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_StateId",
                table: "Card",
                newName: "IX_Card_ColumnId");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "Chat",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId1",
                table: "Card",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Column",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OrderNumber = table.Column<int>(type: "integer", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Column", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Column_Board_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_BoardId1",
                table: "Card",
                column: "BoardId1");

            migrationBuilder.CreateIndex(
                name: "IX_Column_BoardId",
                table: "Column",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Board_BoardId1",
                table: "Card",
                column: "BoardId1",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Column_ColumnId",
                table: "Card",
                column: "ColumnId",
                principalTable: "Column",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Board_BoardId1",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Column_ColumnId",
                table: "Card");

            migrationBuilder.DropTable(
                name: "Column");

            migrationBuilder.DropIndex(
                name: "IX_Card_BoardId1",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "BoardId1",
                table: "Card");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "Chat",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "ColumnId",
                table: "Card",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_ColumnId",
                table: "Card",
                newName: "IX_Card_StateId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Chat",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "ExecutorsWithRights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExecutorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rights = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutorsWithRights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExecutorsWithRights_Board_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BoardId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.Id);
                    table.ForeignKey(
                        name: "FK_State_Board_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Board",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StateState",
                columns: table => new
                {
                    NextStatesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrevStatesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateState", x => new { x.NextStatesId, x.PrevStatesId });
                    table.ForeignKey(
                        name: "FK_StateState_State_NextStatesId",
                        column: x => x.NextStatesId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StateState_State_PrevStatesId",
                        column: x => x.PrevStatesId,
                        principalTable: "State",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExecutorsWithRights_BoardId",
                table: "ExecutorsWithRights",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_State_BoardId",
                table: "State",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_StateState_PrevStatesId",
                table: "StateState",
                column: "PrevStatesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_State_StateId",
                table: "Card",
                column: "StateId",
                principalTable: "State",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
