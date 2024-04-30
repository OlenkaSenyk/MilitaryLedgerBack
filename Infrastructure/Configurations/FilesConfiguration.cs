using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File = Domain.Entities.File;

namespace Infrastructure.Configurations
{
    public class FilesConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("Files");
            builder.HasKey(file => file.Id);
            builder.Property(file => file.Id).ValueGeneratedOnAdd();
            builder.Property(file => file.Photo).IsRequired();
            builder.Property(file => file.Passport).IsRequired();
            builder.Property(file => file.IndividualTaxNumber).IsRequired();
            builder.Property(file => file.MedicalCertificate).IsRequired();
            builder.Property(file => file.ResidencePermit).IsRequired();
            builder.Property(file => file.CreatedAt).IsRequired();
            builder.Property(file => file.CreatedById).IsRequired();
            builder.Property(file => file.LastUpdatedAt).IsRequired();
            builder.Property(file => file.LastUpdatedById).IsRequired();
        }
    }
}
