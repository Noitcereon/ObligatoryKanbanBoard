using System.Collections.Generic;
using System.Threading.Tasks;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.Services.Interfaces
{
    /// <summary>
    /// Provides full database access to a KanbanBoard (CRUD operations).
    /// </summary>
    public interface IKanbanRepository : IKanbanBoardGet
    {
        int AddItem(KanbanItem item);
        Task MoveItemAsync(int itemId, KanbanRepository.Column newColumn);
        Task<bool> DeleteItem(int itemId);
    }
}