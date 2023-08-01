using System.Text.Json.Serialization;

namespace MvcApp.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseNumber { get; set; } = string.Empty;

        public string? Name { get; set; }
        public string? Duration { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        // public string? ImagePath { get; set; }
    }
}