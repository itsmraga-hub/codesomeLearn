using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.responses
{
    public class ModuleResponseDTO
    {
        public int Id { get; set; }
        public string ModuleId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Foreign key to associate with the course
        public int CourseId { get; set; } = 0;
        public CourseResponseDTO Course { get; set; } = new CourseResponseDTO();

        // Navigation property for related lessons
        public List<LessonResponseDTO> Lessons { get; set; } = new List<LessonResponseDTO>();
    }
}
