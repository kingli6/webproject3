namespace College_API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        // public int EducationId { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        // [ForeignKey("EducationId")]
        // public Education Education { get; set; } = new Education();//user can apply to one course...
        public ICollection<EnrolledStudents> EnrolledCourses { get; set; } = new List<EnrolledStudents>(); //not sure if this is right, to store courses the student is enrolled in...
    }
}