using Microsoft.EntityFrameworkCore.Migrations;

namespace KanbanBoardMVCApp.Data.Migrations
{
    public partial class ForeginKeyUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanColumns_KanbanBoards_fk_kanban_board_id",
                table: "KanbanColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanItems_KanbanColumns_fk_column_id",
                table: "KanbanItems");

            migrationBuilder.RenameColumn(
                name: "fk_column_id",
                table: "KanbanItems",
                newName: "KanbanColumnId");

            migrationBuilder.RenameIndex(
                name: "IX_KanbanItems_fk_column_id",
                table: "KanbanItems",
                newName: "IX_KanbanItems_KanbanColumnId");

            migrationBuilder.RenameColumn(
                name: "fk_kanban_board_id",
                table: "KanbanColumns",
                newName: "KanbanBoardId");

            migrationBuilder.RenameIndex(
                name: "IX_KanbanColumns_fk_kanban_board_id",
                table: "KanbanColumns",
                newName: "IX_KanbanColumns_KanbanBoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanColumns_KanbanBoards_KanbanBoardId",
                table: "KanbanColumns",
                column: "KanbanBoardId",
                principalTable: "KanbanBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanItems_KanbanColumns_KanbanColumnId",
                table: "KanbanItems",
                column: "KanbanColumnId",
                principalTable: "KanbanColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KanbanColumns_KanbanBoards_KanbanBoardId",
                table: "KanbanColumns");

            migrationBuilder.DropForeignKey(
                name: "FK_KanbanItems_KanbanColumns_KanbanColumnId",
                table: "KanbanItems");

            migrationBuilder.RenameColumn(
                name: "KanbanColumnId",
                table: "KanbanItems",
                newName: "fk_column_id");

            migrationBuilder.RenameIndex(
                name: "IX_KanbanItems_KanbanColumnId",
                table: "KanbanItems",
                newName: "IX_KanbanItems_fk_column_id");

            migrationBuilder.RenameColumn(
                name: "KanbanBoardId",
                table: "KanbanColumns",
                newName: "fk_kanban_board_id");

            migrationBuilder.RenameIndex(
                name: "IX_KanbanColumns_KanbanBoardId",
                table: "KanbanColumns",
                newName: "IX_KanbanColumns_fk_kanban_board_id");

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanColumns_KanbanBoards_fk_kanban_board_id",
                table: "KanbanColumns",
                column: "fk_kanban_board_id",
                principalTable: "KanbanBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KanbanItems_KanbanColumns_fk_column_id",
                table: "KanbanItems",
                column: "fk_column_id",
                principalTable: "KanbanColumns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
