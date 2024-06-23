using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MVCProject_API.Helpers.Configurations;
using MVCProject_API.Models;
using System;
using System.Reflection;

namespace MVCProject_API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Course>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Instructor>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Student>().HasQueryFilter(m => !m.SoftDeleted);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
