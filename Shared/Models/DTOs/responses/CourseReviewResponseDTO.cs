using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.responses
{
    public class CourseReviewResponseDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CourseReviewId { get; set; } = string.Empty;
        public string ReviewText { get; set; } = string.Empty;

        // Foreign key to associate with the course
        public int CourseId { get; set; }
        public CourseResponseDTO Course { get; set; } = new();

        // Foreign key to associate with the user (student)
        public int UserId { get; set; }
        public UserResponseDTO User { get; set; } = new();
    }
}
