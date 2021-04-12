using Microsoft.EntityFrameworkCore.Migrations;

namespace KanbanBoardMVCApp.Data.Migrations
{
    public partial class IdentityFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>("Id", "KanbanBoards", nullable:false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
