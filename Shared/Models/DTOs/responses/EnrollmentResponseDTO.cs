using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.responses
{
    public class EnrollmentResponseDTO
    {
        public int Id { get; set; }
        public string EnrollmentId { get; set; } = null!;

        public UserResponseDTO User { get; set; } = new UserResponseDTO();

        public Course Course { get; set; } = new Course();

        public DateTime EnrollmentDate { get; set; } = new DateTime();

        public Lesson CurrentLesson { get; set; } = new Lesson();
    }
}
