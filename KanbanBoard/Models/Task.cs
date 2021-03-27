using System.ComponentModel.DataAnnotations;

namespace KanbanBoardMVCApp.Models
{
    public class Task
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        [Required]
        public KanbanColumn Column { get; set; }
    }
}
