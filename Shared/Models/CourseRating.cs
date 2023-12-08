using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class CourseRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CourseRatingId { get; set; } = string.Empty;
        public int Rating { get; set; }

        // Foreign key to associate with the course
        public int CourseId { get; set; }
        public Course Course { get; set; } = new();

        // Foreign key to associate with the user (student)
        public int UserId { get; set; }
        public User User { get; set; } = new User();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
