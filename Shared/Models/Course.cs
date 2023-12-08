using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models
{
    public class Course
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public decimal Price { get; set; } = decimal.Zero;
        public bool IsPublished { get; set; } = false;

        // Navigation property for related modules
        public List<Module> Modules { get; set; } = new List<Module>();

        // Navigation property for course ratings and reviews
        public List<CourseRatingResponseDTO> Ratings { get; set; } = new List<CourseRatingResponseDTO>();
        public List<CourseReviewResponseDTO> CourseReviews { get; set; } = new List<CourseReviewResponseDTO>();

        public List<CommentResponseDTO> Comments { get; set; } = new List<CommentResponseDTO>();

        public List<User> Students { get; set;} = new List<User>();

        public int AuthorID { get; set; } = -1;
        public User Author { get; set; } = new User();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;

        public bool Status { get; set; } = true;

    }
}