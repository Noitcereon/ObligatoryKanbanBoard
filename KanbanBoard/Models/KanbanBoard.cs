﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KanbanBoardMVCApp.Models
{
    public class KanbanBoard
    {
        [Required]
        public int Id { get; set; }

        public string ProjectName { get; set; }

        [Required]
        public List<KanbanColumn> Columns { get; set; }

    }
}
