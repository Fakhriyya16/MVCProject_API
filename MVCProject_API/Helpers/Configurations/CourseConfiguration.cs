using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject_API.Models;

namespace MVCProject_API.Helpers.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.CategoryId).IsRequired();
            builder.Property(m => m.Price).IsRequired();
            builder.Property(m => m.Rating).IsRequired();
            builder.Property(m => m.StartDate).IsRequired();
            builder.Property(m => m.EndDate).IsRequired();
            builder.Property(m => m.InstructorId).IsRequired();
        }
    }
}
