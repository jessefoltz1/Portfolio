using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarRepairWebApi.Models
{
    public class EmployeeAdminRegisterViewModel
    {
        [Required]
        [StringLength(32, ErrorMessage = "First Name must be 32 charcters or less")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Last Name must be 32 charcters or less")]
        public string LastName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "Last Name must be 32 charcters or less")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters")]
        public string Password { get; set; }

        [Required] 
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
