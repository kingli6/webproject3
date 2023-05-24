using College_API.Models;
using Microsoft.EntityFrameworkCore;

namespace College_API.Data
{
    public class CollegeDatabaseContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Course> Courses => Set<Course>();

        public CollegeDatabaseContext(DbContextOptions options) : base(options) { }
    }
}