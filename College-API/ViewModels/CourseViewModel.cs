using College_API.Models;

namespace College_API.ViewModels
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseNumber { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Duration { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public int EnrolledStudents { get; set; }

        public List<Registration> Registrations { get; set; }
        // public string? ImagePath { get; set; }
        // public ICollection<User>? Users { get; set; } = new List<User>();
    }
}