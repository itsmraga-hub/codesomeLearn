using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class Lesson
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LessonId { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = null!;
        public int DurationMinutes { get; set; } = 0;

        // Foreign key to associate with the module
        [ForeignKey(nameof(Module))]
        public int ModuleId { get; set; } = 0;
        public Module Module { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
