using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using codesome.Shared.Models;

namespace codesome.Server.Data
{
    public class codesomeServerContext : DbContext
    {
        public codesomeServerContext (DbContextOptions<codesomeServerContext> options)
            : base(options)
        {
        }

        public DbSet<Course>? Course { get; set; } = default!;

        public DbSet<User>? User { get; set; }

        public DbSet<Comment>? Comment { get; set; }

        public DbSet<Enrollment>? Enrollment { get; set; }

        public DbSet<Lesson>? Lesson { get; set; }

        public DbSet<Module>? Module { get; set; }

        public DbSet<CourseRating>? CourseRating { get; set; }

        public DbSet<CourseReview>? CourseReview { get; set; }
    }
}
