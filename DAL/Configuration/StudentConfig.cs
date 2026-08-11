using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EduVibe.Models.Entities;

namespace EduVibe.Configuration
{
    internal class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.HasKey(p => p.Id);

            builder.Property(x => x.Fname)
                   .HasColumnType("NVarchar")
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(x => x.Lname)
                .HasColumnType("NVarchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.OwnsOne(x => x.Address, a =>
            {
                a.Property(x => x.City)
                .HasColumnType("NVarchar")
                .HasMaxLength(50);

                a.Property(x => x.Country)
                .HasColumnType("NVarchar")
                .HasMaxLength(50);
            });

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnType("NVarchar");

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnType("NVarchar");

            builder.Property(x => x.DateOfBirth)
                .HasColumnType("Date")
                .IsRequired();
        }
    }
}
