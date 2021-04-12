using Microsoft.EntityFrameworkCore.Migrations;

namespace KanbanBoardMVCApp.Data.Migrations
{
    public partial class KBColumnIdentityRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("Id", "KanbanColumns", "IdKBColumn");
            migrationBuilder.AddColumn<int>("Id", "KanbanColumns", nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
