using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.Services
{
    public interface IKanbanRepository
    {
        Task<KanbanBoard> FetchKanbanBoardAsync(int kanbanBoardId);
        Task<List<KanbanItem>> FetchItemsByColumnAsync(KanbanRepository.Column columnId);
        Task<List<KanbanColumn>> FetchColumnsAsync(int kanbanBoardId);
        int AddItem(KanbanItem item);
        Task MoveItemAsync(int itemId, KanbanRepository.Column newColumn);
        Task<KanbanColumn> FetchColumnByIdAsync(int columnId);
    }
}