using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.Services.Interfaces
{
    /// <summary>
    /// All the get methods needed for a kanbanboard.
    /// </summary>
    public interface IKanbanBoardGet
    {
        Task<KanbanBoard> FetchKanbanBoardAsync(int kanbanBoardId);
        Task<List<KanbanItem>> FetchItemsByColumnAsync(KanbanRepository.Column columnId);
        Task<List<KanbanColumn>> FetchColumnsAsync(int kanbanBoardId);
        Task<KanbanColumn> FetchColumnByIdAsync(int columnId);
    }
}
