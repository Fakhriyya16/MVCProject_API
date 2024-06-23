using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject_API.Models;

namespace MVCProject_API.Helpers.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.Property(m => m.FullName).IsRequired();
            builder.Property(m => m.Image).IsRequired();
            builder.Property(m => m.Bio).IsRequired();
        }
    }
}
