using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using College_API.Models;
using Microsoft.EntityFrameworkCore;

namespace College_API.Data
{
    public class CollegeDatabaseContext : DbContext
    {
        public DbSet<Education> Educations => Set<Education>();

        public CollegeDatabaseContext(DbContextOptions options) : base(options) { }
    }
}