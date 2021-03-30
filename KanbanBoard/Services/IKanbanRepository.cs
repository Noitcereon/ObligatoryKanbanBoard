using System.Collections.Generic;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.Services
{
    public interface IKanbanRepository
    {
        KanbanBoard FetchKanbanBoard();
        List<KanbanItem> FetchItemsByColumn(KanbanRepository.Column columnId);
        List<KanbanColumn> FetchColumns(int kanbanBoardId);
    }
}