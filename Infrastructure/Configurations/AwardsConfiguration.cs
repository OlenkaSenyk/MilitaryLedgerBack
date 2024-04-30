using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class AwardsConfiguration : IEntityTypeConfiguration<Award>
    {
        public void Configure(EntityTypeBuilder<Award> builder)
        {
            builder.ToTable("Awards");
            builder.HasKey(award => award.Id);
            builder.Property(award => award.Id).ValueGeneratedOnAdd();
            builder.Property(award => award.Name).HasMaxLength(200).IsRequired();
            builder.Property(award => award.Date).IsRequired();
            builder.Property(award => award.Reason).IsRequired();
            builder.Property(award => award.CreatedAt).IsRequired();
            builder.Property(award => award.CreatedById).IsRequired();
            builder.Property(award => award.LastUpdatedAt).IsRequired();
            builder.Property(award => award.LastUpdatedById).IsRequired();
        }
    }
}
