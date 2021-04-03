using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KanbanBoardMVCApp.Models
{
    public class KanbanBoard
    {
        public int Id { get; set; }

        public string ProjectName { get; set; }

        public override string ToString()
        {
            return ProjectName;
        }
    }
}
