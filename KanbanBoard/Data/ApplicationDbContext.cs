using KanbanBoardMVCApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoardMVCApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<KanbanBoard> KanbanBoards { get; set; }
        public DbSet<KanbanColumn> KanbanColumns { get; set; }
        public DbSet<KanbanItem> KanbanItems { get; set; }
    }
}
