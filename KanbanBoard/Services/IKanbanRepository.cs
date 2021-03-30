using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.Services
{
    public interface IKanbanRepository
    {
        Task<KanbanBoard> FetchKanbanBoardAsync();
        Task<List<KanbanItem>> FetchItemsByColumnAsync(KanbanRepository.Column columnId);
        Task<List<KanbanColumn>> FetchColumnsAsync(int kanbanBoardId);
    }
}