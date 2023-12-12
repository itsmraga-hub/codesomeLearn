using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class Comment
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string text { get; set; } = null!;
        public int Rating { get; set; }

        // Navigation properties
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public Course Course { get; set; } = new();

        [ForeignKey(nameof(CustomUser))]
        public int userId { get; set; }
        public CustomUser user { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
