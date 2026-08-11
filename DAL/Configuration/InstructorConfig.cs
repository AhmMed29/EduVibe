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
    internal class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructor");

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

            builder.Property(i => i.Salary)
                .HasColumnType("Decimal(18,2)")
                .IsRequired(false);

        }
    }
}
