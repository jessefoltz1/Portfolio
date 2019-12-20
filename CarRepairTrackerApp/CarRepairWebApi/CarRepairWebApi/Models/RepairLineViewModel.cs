using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairWebApi.Models
{
    public class RepairLineViewModel
    {
        [Required]
        public int IncidentId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Decimal Cost { get; set; }

        [Required]
        public int TimeHours { get; set; }
    }
}
