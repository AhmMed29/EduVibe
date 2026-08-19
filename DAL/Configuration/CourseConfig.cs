using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduVibe.Models.Entities;

namespace EduVibe.Configuration
{
    internal class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("Course");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Title)
                .HasColumnType("NVarchar")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasColumnType("NVarchar")
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Price)
                .IsRequired();

            builder.Property(c => c.CourseLevel)
                .HasColumnType("NVarchar")
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(c => c.DurationInHours)
                .HasColumnType("int");
        }
    }
}
