namespace College_API.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public User? User { get; set; }

        public Course? Course { get; set; }
    }
}