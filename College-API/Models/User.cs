using System.ComponentModel.DataAnnotations.Schema;

namespace College_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        // public int CourseId { get; set; }
        // [ForeignKey("CourseId")]
        // public ICollection<Course>? Course { get; set; } //user can apply to one course...
        // public ICollection<EnrolledStudents> EnrolledCourses { get; set; } = new List<EnrolledStudents>(); //not sure if this is right, to store courses the student is enrolled in...
    }
}

