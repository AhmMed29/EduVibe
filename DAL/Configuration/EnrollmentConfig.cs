using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduVibe.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduVibe.Configuration
{
    internal class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.ToTable("Enrollment");
            builder.HasKey(e => new {e.StudentId, e.CourseId});

            builder.Property(x=>x.StudentId)
                .HasColumnType("Int")
                .IsRequired();

            builder.Property(x => x.CourseId)
                .HasColumnType("Int")
                .IsRequired();

            builder.Property(x => x.Grades)
                .HasColumnType("Decimal(5,2)")
                .HasDefaultValue(0);

            builder.HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
