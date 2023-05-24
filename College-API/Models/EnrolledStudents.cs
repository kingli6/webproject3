namespace College_API.Models
{
    public class EnrolledStudents
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}