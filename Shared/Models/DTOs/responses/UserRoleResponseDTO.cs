using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.responses
{
    public class UserRoleResponseDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        // public string UserRoleId { get; set; } = string.Empty;

        // Foreign key to associate with the user
        // public int CustomUserId { get; set; }
        public UserResponseDTO CustomUser { get; set; } = new();

        // Role name (e.g., "Student" or "Instructor")
        public string RoleName { get; set; } = string.Empty;
    }

}
