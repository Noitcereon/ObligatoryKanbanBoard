using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBoardMVCApp.Models
{
    public class KanbanColumn
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Headline { get; set; }

        [Required]
        [ForeignKey("fk_kanban_board_id")]
        public KanbanBoard KanbanBoard { get; set; }

        public KanbanColumn()
        {
            
        }

        public KanbanColumn(string headline, KanbanBoard kanbanBoard)
        {
            Headline = headline;
            KanbanBoard = kanbanBoard;
        }

        public override string ToString()
        {
            return Headline;
        }
    }
}
