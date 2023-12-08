using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.responses
{
    public class UserResponseDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }

        // Navigation properties for user roles and enrollments
        public List<UserRoleResponseDTO> UserRoles { get; set; } = new List<UserRoleResponseDTO>();
        public List<EnrollmentResponseDTO> Enrollments { get; set; } = new List<EnrollmentResponseDTO>();

        public List<CourseResponseDTO> Courses { get; set; } = new List<CourseResponseDTO>();
    }
}
