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
        public enum Column
        {
            ToDo = 1, Doing, Testing, Done
        }

        private readonly ApplicationDbContext _context;

        public KanbanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public KanbanBoard FetchKanbanBoard()
        {
            return _context.KanbanBoards.First();
        }


        /// <summary>
        /// Fetches the items from 1 column (To do or Doing or...)
        /// If multiple kanbanboards need to be added, this method needs to change.
        /// </summary>
        /// <param name="column">The column to retrieve items from</param>
        /// <returns></returns>
        public List<KanbanItem> FetchItemsByColumn(Column column)
        {
            return _context.Tasks.Where(t => t.Column.Id == (int)column).ToList();
        }

        public List<KanbanColumn> FetchColumns(int kanbanBoardId)
        {
            return _context.KanbanColumns.Where(x => x.KanbanBoard.Id == kanbanBoardId).ToList();
        }
    }
}
