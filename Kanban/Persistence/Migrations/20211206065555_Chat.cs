using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class Chat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutorsWithRights_Table_TableId",
                table: "ExecutorsWithRights");

            migrationBuilder.DropForeignKey(
                name: "FK_State_Table_TableId",
                table: "State");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Table_TableId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "Table");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "Task",
                newName: "BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_TableId",
                table: "Task",
                newName: "IX_Task_BoardId");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "State",
                newName: "BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_State_TableId",
                table: "State",
                newName: "IX_State_BoardId");

            migrationBuilder.RenameColumn(
                name: "TableId",
                table: "ExecutorsWithRights",
                newName: "BoardId");

            migrationBuilder.RenameIndex(
                name: "IX_ExecutorsWithRights_TableId",
                table: "ExecutorsWithRights",
                newName: "IX_ExecutorsWithRights_BoardId");

            migrationBuilder.CreateTable(
                name: "Board",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Board_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Board_Project_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    App = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Board_ProjectId",
                table: "Board",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Board_ProjectId1",
                table: "Board",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutorsWithRights_Board_BoardId",
                table: "ExecutorsWithRights",
                column: "BoardId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_State_Board_BoardId",
                table: "State",
                column: "BoardId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Board_BoardId",
                table: "Task",
                column: "BoardId",
                principalTable: "Board",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExecutorsWithRights_Board_BoardId",
                table: "ExecutorsWithRights");

            migrationBuilder.DropForeignKey(
                name: "FK_State_Board_BoardId",
                table: "State");

            migrationBuilder.DropForeignKey(
                name: "FK_Task_Board_BoardId",
                table: "Task");

            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "Task",
                newName: "TableId");

            migrationBuilder.RenameIndex(
                name: "IX_Task_BoardId",
                table: "Task",
                newName: "IX_Task_TableId");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "State",
                newName: "TableId");

            migrationBuilder.RenameIndex(
                name: "IX_State_BoardId",
                table: "State",
                newName: "IX_State_TableId");

            migrationBuilder.RenameColumn(
                name: "BoardId",
                table: "ExecutorsWithRights",
                newName: "TableId");

            migrationBuilder.RenameIndex(
                name: "IX_ExecutorsWithRights_BoardId",
                table: "ExecutorsWithRights",
                newName: "IX_ExecutorsWithRights_TableId");

            migrationBuilder.CreateTable(
                name: "Table",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProjectId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Table_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Table_Project_ProjectId1",
                        column: x => x.ProjectId1,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Table_ProjectId",
                table: "Table",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Table_ProjectId1",
                table: "Table",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExecutorsWithRights_Table_TableId",
                table: "ExecutorsWithRights",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_State_Table_TableId",
                table: "State",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Table_TableId",
                table: "Task",
                column: "TableId",
                principalTable: "Table",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
