using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBoardMVCApp.Models
{
    public class KanbanItem
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Title { get; set; }

        public int KanbanColumnId { get; set; }
        public KanbanColumn KanbanColumn { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
