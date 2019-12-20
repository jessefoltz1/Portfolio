using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairWebApi.Models
{
    public class VehicleIncidentViewModel
    {
        [Required]
        [StringLength(17, ErrorMessage = "Vin numbers can be more than 17 characters")]
        public string Vin { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
