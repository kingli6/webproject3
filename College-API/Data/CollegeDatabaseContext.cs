using College_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace College_API.Data
{
    public class CollegeDatabaseContext : IdentityDbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Category> Categories => Set<Category>();

        public CollegeDatabaseContext(DbContextOptions options) : base(options) { }
    }
}