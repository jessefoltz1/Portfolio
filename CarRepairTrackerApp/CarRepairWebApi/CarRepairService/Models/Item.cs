using System;
using System.Collections.Generic;
using System.Text;

namespace CarRepairService.Models
{
    public class Item
    {
        public const int InvalidId = -1;

        public int Id { get; set; } = InvalidId;
    }
}
