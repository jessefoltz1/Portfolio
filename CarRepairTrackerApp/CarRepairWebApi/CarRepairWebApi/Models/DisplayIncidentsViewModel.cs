using CarRepairService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairWebApi.Models
{
    public class DisplayIncidentsViewModel
    {
        public Incident Incident { get; set; }
        public Vehicle Vehicle { get; set; }
        public string Status { get; set; }
    }
}
