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
    public class DocumentsConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");
            builder.HasKey(document => document.Id);
            builder.Property(document => document.Id).ValueGeneratedOnAdd();
            builder.Property(document => document.Type).HasMaxLength(200).IsRequired();
            builder.Property(document => document.Number).HasMaxLength(100).IsRequired();
            builder.Property(document => document.IssueDate).IsRequired();
            builder.Property(document => document.ExpiryDate);
            builder.Property(document => document.ScanCopy).IsRequired();
            builder.Property(document => document.CreatedAt).IsRequired();
            builder.Property(document => document.CreatedById).IsRequired();
            builder.Property(document => document.LastUpdatedAt).IsRequired();
            builder.Property(document => document.LastUpdatedById).IsRequired();
        }
    }
}
