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
    public class CourseScheduleConfig : IEntityTypeConfiguration<CourseSchedule>
    {
        public void Configure(EntityTypeBuilder<CourseSchedule> builder)
        {
            builder.ToTable("CourseSchedule");
            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.StartTime)
                .IsRequired()
                .HasColumnType("time");

            builder.Property(cs => cs.EndTime)
                .IsRequired()
                .HasColumnType("time");

            builder.HasOne(x=>x.Course)
                .WithMany(x=>x.CourseSchedules)
                .HasForeignKey(x=>x.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
