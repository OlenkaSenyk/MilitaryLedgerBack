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
    public class DocumentTemplatesConfiguration : IEntityTypeConfiguration<DocumentTemplate>
    {
        public void Configure(EntityTypeBuilder<DocumentTemplate> builder)
        {
            builder.ToTable("DocumentTemplates");
            builder.HasKey(template => template.Id);
            builder.Property(template => template.Id).ValueGeneratedOnAdd();
            builder.Property(template => template.Name).HasMaxLength(200).IsRequired();
            builder.Property(template => template.Type).HasMaxLength(200).IsRequired();
            builder.Property(template => template.Content).IsRequired();
            builder.Property(template => template.CreatedAt).IsRequired();
            builder.Property(template => template.CreatedById).IsRequired();
            builder.Property(template => template.LastUpdatedAt).IsRequired();
            builder.Property(template => template.LastUpdatedById).IsRequired();
        }
    }
}
