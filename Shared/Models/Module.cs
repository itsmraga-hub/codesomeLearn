using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ModuleId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        // Foreign key to associate with the course
        public int CourseId { get; set; } = 0;
        public Course Course { get; set; } = new Course();

        // Navigation property for related lessons
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
