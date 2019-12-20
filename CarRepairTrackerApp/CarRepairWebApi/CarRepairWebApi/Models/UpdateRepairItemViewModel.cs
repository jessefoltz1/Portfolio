using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairWebApi.Models
{
    public class UpdateRepairItemViewModel
    {
        [Required]
        public int LineId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Decimal Cost { get; set; }

        [Required]
        public int TimeHours { get; set; }
    }
}
