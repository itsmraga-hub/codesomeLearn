using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class CommentResponseDTO
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string text { get; set; } = null!;
        public int Rating { get; set; }

        // Navigation properties
        public int CourseId { get; set; }
        public Course Course { get; set; } = new();

        public int userId { get; set; }
        public CustomUser user { get; set; } = new();
    }
}
