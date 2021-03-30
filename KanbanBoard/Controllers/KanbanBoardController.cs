using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanBoardMVCApp.Data;
using KanbanBoardMVCApp.Models;
using KanbanBoardMVCApp.Services;
using KanbanBoardMVCApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient.DataClassification;

namespace KanbanBoardMVCApp.Controllers
{
    [Authorize]
    public class KanbanBoardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IKanbanRepository _repos;

        public KanbanBoardController(ApplicationDbContext context, IKanbanRepository repos)
        {
            _context = context;
            _repos = repos;
        }

        // GET: KanbanBoard
        public async Task<IActionResult> Index()
        {
            KanbanBoard kanbanBoard = _repos.FetchKanbanBoard();
            List<KanbanColumn> columns = _repos.FetchColumns(kanbanBoard.Id);
            List<KanbanItem> items = _repos.FetchTaskItems()
            return View(new KanbanBoardVM());
        }

     
    }
}
