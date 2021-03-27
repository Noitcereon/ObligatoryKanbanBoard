using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KanbanBoardMVCApp.Data;
using KanbanBoardMVCApp.Models;

namespace KanbanBoardMVCApp.Controllers
{
    public class KanbanBoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KanbanBoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KanbanBoards
        public async Task<IActionResult> Index()
        {
            return View(await _context.KanbanBoards.ToListAsync());
        }

     
    }
}
