using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoardMVCApp.Data;
using KanbanBoardMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoardMVCApp.Services
{
    
    public class KanbanRepository : IKanbanRepository
    {
        private readonly ApplicationDbContext _context;

        public KanbanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public enum Column
        {
            ToDo = 1, Doing, Testing, Done
        }

        public KanbanBoard FetchKanbanBoard()
        {
            return _context.KanbanBoards.First();
        }
       
        public List<KanbanItem> FetchItemsByColumn(int columnId)
        {
            return _context.Tasks.Where(t => t.Id == columnId).ToList();
        }

        public List<KanbanColumn> FetchColumns(int kanbanBoardId)
        {
            return _context.KanbanColumns.Where(x => x.KanbanBoard.Id == kanbanBoardId).ToList();
        }
    }
}
