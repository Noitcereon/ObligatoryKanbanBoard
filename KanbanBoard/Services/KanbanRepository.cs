using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using KanbanBoardMVCApp.Data;
using KanbanBoardMVCApp.Models;
using KanbanBoardMVCApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace KanbanBoardMVCApp.Services
{

    public class KanbanRepository : IKanbanRepository
    {
        public enum Column
        {
            ToDo = 1, Doing, Testing, Done
        }

        private readonly ApplicationDbContext _context;
        private readonly ILogger<KanbanRepository> _logger;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        public KanbanRepository(ApplicationDbContext context, ILogger<KanbanRepository> logger,
            IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;
            _emailSender = emailSender;
            _userManager = userManager; // used when fetching emails for email sending.
        }

        public async Task<KanbanBoard> FetchKanbanBoardAsync(int kanbanBoardId = 1)
        {
            try
            {
                return await _context.KanbanBoards.FirstAsync(x => x.Id == kanbanBoardId);
            }
            catch (Exception e)
            {
                _logger.LogCritical($"{e.Source}: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Fetches the items from 1 column (To do or Doing or...)
        /// If multiple kanbanboards need to be added, this method needs to change.
        /// </summary>
        /// <param name="column">The column to retrieve items from</param>
        /// <returns></returns>
        public async Task<List<KanbanItem>> FetchItemsByColumnAsync(Column column)
        {
            return await _context.KanbanItems.Where(t => t.KanbanColumnId == (int)column).ToListAsync();
        }

        public async Task<List<KanbanColumn>> FetchColumnsAsync(int kanbanBoardId)
        {
            return await _context.KanbanColumns.Where(x => x.KanbanBoardId == kanbanBoardId).ToListAsync();
        }

        public int AddItem(KanbanItem item)
        {
            _context.Add(item);
            return _context.SaveChanges();
        }

        public async Task MoveItemAsync(int itemId, Column newColumn)
        {
            var itemToUpdate = await _context.KanbanItems.FirstOrDefaultAsync(ki => ki.Id == itemId);
            if (itemToUpdate != null)
            {
                itemToUpdate.KanbanColumnId = _context.KanbanColumns.Find((int)newColumn).Id;
                int affectedColumns = _context.SaveChanges();

                if (newColumn == Column.Done && affectedColumns > 0)
                {
                    var recipents = await FetchTeamMembersAsync();

                    List<string> emails = recipents.Select(user => user.Email).ToList();
                    await Task.Run(() => SendEmails(emails, itemToUpdate));
                }
            }
            // Needs Exception handling / handling of itemToUpdate == null.
        }

        
        public async Task<int> UpdateItem(KanbanItem item)
        {
            _context.Update(item);
            if (item.KanbanColumnId == (int) Column.Done)
            {
                var teamMembers = await FetchTeamMembersAsync();
                List<string> emails = teamMembers.Select(user => user.Email).ToList();
                SendEmails(emails, item);
            }
            return await _context.SaveChangesAsync();
        }

        public async Task<KanbanColumn> FetchColumnByIdAsync(int columnId)
        {
            return await _context.KanbanColumns.FindAsync(columnId);
        }

        public bool DeleteItem(int itemId)
        {
            try
            {
                KanbanItem itemToDelete = _context.KanbanItems.Find(itemId);
                if (itemToDelete is null)
                {
                    return false;
                }
                _context.KanbanItems.Remove(itemToDelete);
                _context.SaveChanges();
            }

            catch (Exception e)
            {
                _logger.LogError($"{e.Source}: {e.Message}");
                Console.WriteLine("Error in DeleteItem: " + e.Message);
                return false;
            }
            return true;
        }

        private async Task<List<IdentityUser>> FetchTeamMembersAsync()
        {
            IList<IdentityUser> adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            IList<IdentityUser> teamUsers = await _userManager.GetUsersInRoleAsync("Team");
            List<IdentityUser> teamMembers = new List<IdentityUser>();
            teamMembers.AddRange(adminUsers);
            teamMembers.AddRange(teamUsers);
            return teamMembers; 
        }

        /// <summary>
        /// Sends emails to all users with the team or admin role.
        /// Used when a KanbanItem is moved to the Done column.
        /// </summary>
        /// <param name="emails">A list of all the recipents' email addresses.</param>
        /// <param name="item">The updated kanbanitem.</param>
        private void SendEmails(List<string> emails, KanbanItem item)
        {
            Task.Run(() => _emailSender.SendMailToMultipleAsync(emails,
                "KanbanItem moved to Done",
                $"{item.Title} was moved to done column."));
        }
    }
}
