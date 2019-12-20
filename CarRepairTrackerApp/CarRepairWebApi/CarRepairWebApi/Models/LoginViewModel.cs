using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRepairWebApi.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// The user's login username or email.
        /// </summary>
        [Required]
        public string LoginCredentials { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
