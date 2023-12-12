using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class CourseReviewResponseDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CourseReviewId { get; set; } = "";
        public string ReviewText { get; set; } = "";

        // Foreign key to associate with the course
        public int CourseId { get; set; }
        public Course Course { get; set; } = new();

        // Foreign key to associate with the user (student)
        public int CustomUserId { get; set; }
        public CustomUser CustomUser { get; set; } = new();
    }
}
