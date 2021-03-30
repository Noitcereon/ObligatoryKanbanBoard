using System.Collections.Generic;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.Services
{
    public interface IKanbanRepository
    {
        KanbanBoard FetchKanbanBoard();
        List<KanbanItem> FetchTaskItems(int columnId);
        List<KanbanColumn> FetchColumns(int kanbanBoardId);
    }
}