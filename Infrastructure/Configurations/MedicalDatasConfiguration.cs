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
    public class MedicalDatasConfiguration : IEntityTypeConfiguration<MedicalData>
    {
        public void Configure(EntityTypeBuilder<MedicalData> builder)
        {
            builder.ToTable("MedicalDatas");
            builder.HasKey(data => data.Id);
            builder.Property(data => data.Id).ValueGeneratedOnAdd();
            builder.Property(data => data.BloodType).IsRequired();
            builder.Property(data => data.BloodRh).IsRequired();
            builder.Property(data => data.Eligibility).IsRequired();
            builder.Property(data => data.Features).IsRequired();
            builder.Property(data => data.Notes);
            builder.Property(data => data.CreatedAt).IsRequired();
            builder.Property(data => data.CreatedById).IsRequired();
            builder.Property(data => data.LastUpdatedAt).IsRequired();
            builder.Property(data => data.LastUpdatedById).IsRequired();
        }
    }
}
