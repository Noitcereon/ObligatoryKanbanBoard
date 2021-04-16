using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanBoardMVCApp.Data;
using KanbanBoardMVCApp.Models;
using KanbanBoardMVCApp.Services.Interfaces;
using KanbanBoardMVCApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace KanbanBoardMVCApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles = "Admin, Team, Contributor, Observer")]
    public class KanbanItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IKanbanRepository _repos;

        public KanbanItemsController(ApplicationDbContext context, IKanbanRepository repos)
        {
            _context = context;
            _repos = repos;
        }

        // GET: KanbanItems
        public IActionResult Index()
        {
            return RedirectToAction("Index", "KanbanBoard");
        }

        // GET: KanbanItems/Create
        public IActionResult AddItem()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Team, Contributor")]
        public IActionResult AddItem(KanbanItem item)
        {
            if (ModelState.IsValid)
            {
                _repos.AddItem(item);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(AddItem));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateItem(int? itemId)
        {
            if (itemId == null)
            {
                return NotFound();
            }
            var kanbanItem = await _context.KanbanItems.FindAsync(itemId);
            if (kanbanItem == null)
            {
                return NotFound();
            }
            return View(kanbanItem);
        }

        // POST: KanbanItems/Edit/5
        [HttpPost]
        public async Task<IActionResult> UpdateItem(int itemId, [Bind("Id,Title,KanbanColumnId")] KanbanItem kanbanItem)
        {
            if (itemId != kanbanItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repos.UpdateItem(kanbanItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KanbanItemExists(kanbanItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kanbanItem);
        }

        // POST: KanbanItems/Delete/5
        [HttpPost]
        public IActionResult Delete(int itemId)
        {
            var success = _repos.DeleteItem(itemId);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View("Error", new ErrorViewModel{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        
      
        private bool KanbanItemExists(int itemId)
        {
            return _context.KanbanItems.Any(e => e.Id == itemId);
        }
    }
}
