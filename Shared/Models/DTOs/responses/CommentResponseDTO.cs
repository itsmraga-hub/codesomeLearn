using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.responses
{
    public class CommentResponseDTO
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string text { get; set; } = string.Empty;
        public int Rating { get; set; }

        // Navigation properties
        public int CourseId { get; set; }
        public Course Course { get; set; } = new Course();

        public int userId { get; set; }
        public User user { get; set; } = new User();
    }
}
