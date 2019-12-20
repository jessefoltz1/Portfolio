﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairWebApi.Models
{
    public class PayIncidentViewModel
    {
        [Required]
        public int IncidentId { get; set; }

        [Required]
        public DateTime CompletedByDate { get; set; }
    }
}
