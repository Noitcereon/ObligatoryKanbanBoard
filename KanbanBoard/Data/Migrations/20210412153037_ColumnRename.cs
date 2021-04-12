using Microsoft.EntityFrameworkCore.Migrations;

namespace KanbanBoardMVCApp.Data.Migrations
{
    public partial class ColumnRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("IdKBColumn", "KanbanColumns", "KBColumnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
