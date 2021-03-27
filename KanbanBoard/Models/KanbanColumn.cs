using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBoardMVCApp.Models
{
    public class KanbanColumn
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Headline { get; set; }

        [Required]
        [ForeignKey("fk_kanban_board_id")]
        public KanbanBoard KanbanBoard { get; set; }
    }
}
