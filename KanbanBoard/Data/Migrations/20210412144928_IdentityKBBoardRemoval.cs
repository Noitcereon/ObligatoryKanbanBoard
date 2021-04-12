using Microsoft.EntityFrameworkCore.Migrations;

namespace KanbanBoardMVCApp.Data.Migrations
{
    public partial class IdentityKBBoardRemoval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn("Id", "KanbanBoards", "KBId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "KanbanBoards",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");
        }
    }
}
