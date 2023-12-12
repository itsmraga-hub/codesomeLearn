using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class CourseReview
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CourseReviewId { get; set; } = null!;
        public string ReviewText { get; set; } = null!;

        // Foreign key to associate with the course
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; } = new();

        // Foreign key to associate with the user (student)
        [ForeignKey(nameof(CustomUser))]
        public int CustomUserId { get; set; }
        public CustomUser CustomUser { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
