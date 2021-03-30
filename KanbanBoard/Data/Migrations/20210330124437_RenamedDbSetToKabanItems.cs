using Microsoft.EntityFrameworkCore.Migrations;

namespace KanbanBoardMVCApp.Data.Migrations
{
    public partial class RenamedDbSetToKabanItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.CreateTable(
                name: "KanbanItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fk_column_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanItems_KanbanColumns_fk_column_id",
                        column: x => x.fk_column_id,
                        principalTable: "KanbanColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KanbanItems_fk_column_id",
                table: "KanbanItems",
                column: "fk_column_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KanbanItems");

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fk_column_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_KanbanColumns_fk_column_id",
                        column: x => x.fk_column_id,
                        principalTable: "KanbanColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_fk_column_id",
                table: "Tasks",
                column: "fk_column_id");
        }
    }
}
