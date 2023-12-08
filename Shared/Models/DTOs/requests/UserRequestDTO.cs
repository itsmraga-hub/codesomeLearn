using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.requests
{
    public class UserRequestDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Password1 { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }

        // Navigation properties for user roles and enrollments
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        public List<Course> Courses { get; set; } = new List<Course>();
    }
}
