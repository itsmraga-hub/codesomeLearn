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

        public UserResponseDTO CustomUser { get; set; } = new UserResponseDTO();

        public CourseResponseDTO Course { get; set; } = new CourseResponseDTO();

        public DateTime EnrollmentDate { get; set; } = new DateTime();

        public LessonResponseDTO CurrentLesson { get; set; } = new LessonResponseDTO();
    }
}
