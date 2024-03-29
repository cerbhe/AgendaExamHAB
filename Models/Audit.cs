﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgendaExamHAB.Models
{
    public abstract class Audit
    {
        public DateTimeOffset Created { get; set; }
        
        public string? CreatedBy { get; set; }

        public DateTimeOffset Modified { get; set; }

        public string? ModifiedBy { get; set; }
    }
}
