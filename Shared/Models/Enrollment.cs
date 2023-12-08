using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class Enrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EnrollmentId { get; set; } = null!;

        // Foreign key to associate with the user (student)
        public int UserId { get; set; }
        public User User { get; set; } = new User();

        // Foreign key to associate with the course
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();

        public DateTime EnrollmentDate { get; set; } = new DateTime();

        // Progress tracking (e.g., which lesson the user is currently on)
        public int CurrentLessonId { get; set; } = 0;
        public Lesson CurrentLesson { get; set; } = new Lesson();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }

}
