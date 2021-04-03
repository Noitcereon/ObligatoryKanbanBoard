using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBoardMVCApp.Models
{
    public class KanbanItem
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        [ForeignKey("fk_column_id")]
        public KanbanColumn Column { get; set; }


        public override string ToString()
        {
            return Title;
        }
    }
}
