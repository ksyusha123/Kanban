using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Priz1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_State_StateId",
                table: "Card");

            migrationBuilder.DropTable(
                name: "StateState");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Card",
                newName: "ColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_StateId",
                table: "Card",
                newName: "IX_Card_ColumnId");

            migrationBuilder.CreateTable(
                name: "Column",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "ColumnColumn",
                columns: table => new
                {
                    NextStatesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrevStatesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnColumn", x => new { x.NextStatesId, x.PrevStatesId });
                    table.ForeignKey(
                        name: "FK_ColumnColumn_Column_NextStatesId",
                        column: x => x.NextStatesId,
                        principalTable: "Column",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColumnColumn_Column_PrevStatesId",
                        column: x => x.PrevStatesId,
                        principalTable: "Column",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Column_BoardId",
                table: "Column",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnColumn_PrevStatesId",
                table: "ColumnColumn",
                column: "PrevStatesId");

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
                name: "FK_Card_Column_ColumnId",
                table: "Card");

            migrationBuilder.DropTable(
                name: "ColumnColumn");

            migrationBuilder.DropTable(
                name: "Column");

            migrationBuilder.RenameColumn(
                name: "ColumnId",
                table: "Card",
                newName: "StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Card_ColumnId",
                table: "Card",
                newName: "IX_Card_StateId");

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
