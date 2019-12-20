using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairWebApi.Models
{
    public class ApproveRepairViewModel
    {
        [Required]
        public int LineId { get; set; }
    }
}
