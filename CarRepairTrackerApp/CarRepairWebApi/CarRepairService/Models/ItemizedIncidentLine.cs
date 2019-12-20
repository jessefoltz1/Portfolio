using System;
using System.Collections.Generic;
using System.Text;

namespace CarRepairService.Models
{
    [Serializable]
    public class ItemizedIncidentLine : Item
    {
        public int IncidentId { get; set; }
        public string Description { get; set; }
        public Decimal Cost { get; set; }
        public int TimeHours { get; set; }
        public bool Approved { get; set; }
        public bool Declined { get; set; }
    }
}