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
    public class ServiceHistoriesConfiguration : IEntityTypeConfiguration<ServiceHistory>
    {
        public void Configure(EntityTypeBuilder<ServiceHistory> builder)
        {
            builder.ToTable("ServiceHistories");
            builder.HasKey(service => service.Id);
            builder.Property(service => service.Id).ValueGeneratedOnAdd();
            builder.Property(service => service.StartDate).IsRequired();
            builder.Property(service => service.EndDate).IsRequired();
            builder.Property(service => service.MilitaryRank).HasMaxLength(200).IsRequired();
            builder.Property(service => service.MilitaryCategory).HasMaxLength(200).IsRequired();
            builder.Property(service => service.MilitaryBranch).HasMaxLength(200).IsRequired();
            builder.Property(service => service.MilitaryUnit).HasMaxLength(200).IsRequired();
            builder.Property(service => service.CreatedAt).IsRequired();
            builder.Property(service => service.CreatedById).IsRequired();
            builder.Property(service => service.LastUpdatedAt).IsRequired();
            builder.Property(service => service.LastUpdatedById).IsRequired();
        }
    }
}
