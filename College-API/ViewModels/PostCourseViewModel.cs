namespace College_API.ViewModels
{
    public class PostCourseViewModel
    {
        public string? CourseNumber { get; set; }
        public string? Name { get; set; }
        public string? Duration { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int CategoryId { get; set; } // Add this property for CategoryId
        // public string? ImagePath { get; set; }
    }
}