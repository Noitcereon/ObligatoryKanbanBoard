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
        // Note: Tasks was renamed to KanbanItems (might affect database when updating it)
        public DbSet<KanbanItem> KanbanItems { get; set; }
    }
}
