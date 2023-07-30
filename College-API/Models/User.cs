using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace College_API.Models
{
    public class User
    {
        // public User()
        // {
        //     RegisterDate = DateTime.Now;
        // }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime RegisterDate { get; set; }
        // public int CourseId { get; set; }
        // [ForeignKey("CourseId")]
        // public Course Course { get; set; } = new Course();

        // public ICollection<EnrolledStudents> EnrolledCourses { get; set; } = new List<EnrolledStudents>(); //not sure if this is right, to store courses the student is enrolled in...
    }
}

