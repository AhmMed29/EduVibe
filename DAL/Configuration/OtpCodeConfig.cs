using EduVibe.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduVibe.Configuration;

public class OtpCodeConfig : IEntityTypeConfiguration<OtpCode>
{
    public void Configure(EntityTypeBuilder<OtpCode> builder)
    {
        builder.ToTable("OtpCodes");
        builder.HasIndex(x=> new {x.Email, x.Purpose});
    }
}