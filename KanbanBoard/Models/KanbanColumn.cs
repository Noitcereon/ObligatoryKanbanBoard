using System.ComponentModel.DataAnnotations;

namespace KanbanBoardMVCApp.Models
{
    public class KanbanColumn
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Headline { get; set; }

        [Required]
        public KanbanBoard KanbanBoard { get; set; }
    }
}
