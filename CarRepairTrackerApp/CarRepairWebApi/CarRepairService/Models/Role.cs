using System;
using System.Collections.Generic;
using System.Text;

namespace CarRepairService.Models
{
    [Serializable]
    public class Role : Item
    {
        public string Name { get; set; }
    }
}
