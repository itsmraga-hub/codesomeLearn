using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.responses
{
    public class LessonResponseDTO
    {
        public int Id { get; set; }
        public string LessonId { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = null!;
        public int DurationMinutes { get; set; } = 0;

        // Foreign key to associate with the module
        public int ModuleId { get; set; } = 0;
        public Module Module { get; set; } = null!;
    }
}
