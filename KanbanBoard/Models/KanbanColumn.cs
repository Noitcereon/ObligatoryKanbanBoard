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

        public int KanbanBoardId { get; set; }
        public KanbanBoard KanbanBoard { get; set; }

        public KanbanColumn()
        {
            
        }

        public KanbanColumn(string headline, int kanbanBoardId)
        {
            Headline = headline;
            KanbanBoardId = kanbanBoardId;
        }

        public override string ToString()
        {
            return Headline;
        }
    }
}
