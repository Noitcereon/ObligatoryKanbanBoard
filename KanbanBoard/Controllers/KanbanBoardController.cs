﻿using System;
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
using static KanbanBoardMVCApp.Services.KanbanRepository;

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
            KanbanBoard kanbanBoard = await _repos.FetchKanbanBoardAsync(1);
            List<KanbanColumn> columns = await _repos.FetchColumnsAsync(kanbanBoard.Id);
            List<KanbanItem> todoItems = await _repos.FetchItemsByColumnAsync(Column.ToDo);
            List<KanbanItem> doingItems = await _repos.FetchItemsByColumnAsync(Column.Doing);
            List<KanbanItem> testingItems = await _repos.FetchItemsByColumnAsync(Column.Testing);
            List<KanbanItem> doneItems = await _repos.FetchItemsByColumnAsync(Column.Done);
            KanbanBoardVM viewModel = new KanbanBoardVM
            {
                KanbanBoard = kanbanBoard,
                KanbanColumns = columns,
                ToDoItems = todoItems,
                DoingItems = doingItems,
                TestingItems = testingItems,
                DoneItems = doneItems
            };
            return View(viewModel);
        }


        public async Task<IActionResult> MoveItem(int itemId)
        {
            await _repos.MoveItemAsync(itemId, Column.Done);
            return RedirectToAction(nameof(Index));
        }
    }
}
