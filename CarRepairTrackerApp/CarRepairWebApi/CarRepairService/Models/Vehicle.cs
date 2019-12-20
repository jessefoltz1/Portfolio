using System;
using System.Collections.Generic;
using System.Text;

namespace CarRepairService.Models
{
    [Serializable]
    public class Vehicle : Item
    {
        public string Vin { get; set; }
        public int UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
    }
}
