using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject_API.Models;

namespace MVCProject_API.Helpers.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.Property(m => m.FullName).IsRequired();
            builder.Property(m => m.Image).IsRequired();
            builder.Property(m => m.Position).IsRequired();
            builder.Property(m => m.Email).IsRequired();
        }
    }
}
