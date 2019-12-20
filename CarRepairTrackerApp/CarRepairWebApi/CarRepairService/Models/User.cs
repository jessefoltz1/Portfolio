using System;
using System.Collections.Generic;
using System.Text;

namespace CarRepairService.Models
{
    [Serializable]
    public class User : Item
    {
        public int RoleId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}