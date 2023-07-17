using System.ComponentModel.DataAnnotations.Schema;

namespace College_API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseNumber { get; set; } = string.Empty;//four digit. rnd generated?
        public string? Name { get; set; }
        public string? Duration { get; set; }
        // Beskrivning (är en kortare övergripande beskrivning om kursen och varför man ska gå den)
        public string? Description { get; set; }
        // Detaljer (här finns information om vilka moduler/delar kursen går igenom)
        public string? Details { get; set; }
        public string? ImagePath { get; set; }
        // public string? ImageUrl { get; set; }
        // public Category? Category { get; set; }
        // public ICollection<EnrolledStudents> EnrolledStudents { get; set; } = new List<EnrolledStudents>();
        // public int CategoryId { get; set; }
        // [ForeignKey("CategoryId")]
        // public Category Category { get; set; } = new Category();
        public ICollection<User>? Users { get; set; } = new List<User>();
    }
}