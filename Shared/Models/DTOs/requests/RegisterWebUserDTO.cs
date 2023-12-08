using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.requests
{

    public class RegisterWebUserDTO
    {
        [Required]
        [StringLength(15, ErrorMessage = "Name length can't be more than 15.")]
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        [StringLength(10, ErrorMessage = "Phone number can't be more than 10.", MinimumLength = 10)]
        public string Phone { get; set; } = "";
        [Required]
        [StringLength(20, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
        public string Password { get; set; } = "";
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = "";
    }
}
