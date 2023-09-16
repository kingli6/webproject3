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
        public int EnrolledStudents { get; set; }
        public List<Registration>? Registrations { get; set; }
        public string? Category { get; set; }

    }
}