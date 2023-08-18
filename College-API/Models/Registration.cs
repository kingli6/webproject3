using Microsoft.AspNetCore.Identity;

namespace College_API.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public string? UserId { get; set; }
        public int CourseId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public IdentityUser? User { get; set; }
        public Course? Course { get; set; }
    }
}