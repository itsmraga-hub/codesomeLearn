using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace codesome.Shared.Models.DTOs.requests
{
    public class CourseRequestDTO
    {
        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; } = new();
        public DateTime EndDate { get; set; } = new();
        public decimal Price { get; set; } = decimal.Zero;
        public bool IsPublished { get; set; } = false;

        // Navigation property for related modules
        public List<Module> Modules { get; set; } = new List<Module>();

        // Navigation property for course ratings and reviews
        public List<CourseRatingResponseDTO> Ratings { get; set; } = new List<CourseRatingResponseDTO>();
        public List<CourseReviewResponseDTO> CourseReviews { get; set; } = new List<CourseReviewResponseDTO>();

        public List<CommentResponseDTO> Comments { get; set; } = new List<CommentResponseDTO>();
    }
}
