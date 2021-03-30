using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.ViewModels
{
    public class KanbanBoardVM
    {
        public KanbanBoard KanbanBoard { get; set; }
        public List<KanbanColumn> KanbanColumns { get; set; }
        public List<KanbanItem> ToDoItems { get; set; }
        public List<KanbanItem> DoingItems { get; set; }
        public List<KanbanItem> TestingItems { get; set; }
        public List<KanbanItem> DoneItems { get; set; }
     
    }
}
