using Kodlama.io.Devs.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistence.EntityConfigurations
{
    public class GithubProfileConfiguration : IEntityTypeConfiguration<GithubProfile>
    {
        public void Configure(EntityTypeBuilder<GithubProfile> builder)
        {
            builder.ToTable("GithubProfiles");
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.Property(p => p.UserId).HasColumnName("UserId");
            builder.Property(p => p.GithubUrl).HasColumnName("GithubUrl");
            builder.HasOne(p => p.User);
        }
    }
}
