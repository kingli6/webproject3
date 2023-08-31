using College_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace College_API.Data
{
    public class CollegeDatabaseContext : IdentityDbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Registration> Registrations => Set<Registration>();

        public CollegeDatabaseContext(DbContextOptions options) : base(options) { }

    }
}