namespace College_API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseNumber { get; set; } //four digit. rnd generated?
        public string? ImagePath { get; set; }
        public string? ImageUrl { get; set; }
        public string? CourseName { get; set; }
        public string? Duration { get; set; }
        // public Category? Category { get; set; }
        public string? Description { get; set; }
        // Beskrivning (är en kortare övergripande beskrivning om kursen och varför man ska gå den)
        // Detaljer (här finns information om vilka moduler/delar kursen går igenom)
        public string? Details { get; set; }
        public ICollection<EnrolledStudents> EnrolledStudents { get; set; } = new List<EnrolledStudents>();
    }
}