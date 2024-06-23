using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVCProject_API.Models;

namespace MVCProject_API.Helpers.Configurations
{
    public class AboutConfiguration : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(m => m.Heading).IsRequired();
            builder.Property(m => m.Description).IsRequired();
            builder.Property(m => m.Image).IsRequired();
        }
    }
}
