using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.ViewModels
{
    public class KanbanBoardVM
    {
        public List<KanbanItem> TaskItems { get; set; } = new List<KanbanItem>();

        public static KanbanBoard KanbanBoard { get; set; } = new KanbanBoard();
        public List<KanbanColumn> KanbanColumns { get; set; } = new List<KanbanColumn>()
        {
            new KanbanColumn("To Do", KanbanBoard),
            new KanbanColumn("Doing", KanbanBoard),
            new KanbanColumn("Testing", KanbanBoard),
            new KanbanColumn("Done", KanbanBoard)
        };
    }
}
