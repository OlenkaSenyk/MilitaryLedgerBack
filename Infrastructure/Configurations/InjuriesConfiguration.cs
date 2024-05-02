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
    public class InjuriesConfiguration : IEntityTypeConfiguration<Injurie>
    {
        public void Configure(EntityTypeBuilder<Injurie> builder)
        {
            builder.ToTable("Injuries");
            builder.HasKey(injurie => injurie.Id);
            builder.Property(injurie => injurie.Id).ValueGeneratedOnAdd();
            builder.Property(injurie => injurie.Date).IsRequired();
            builder.Property(injurie => injurie.Type).IsRequired();
            builder.Property(injurie => injurie.Location).IsRequired();
            builder.Property(injurie => injurie.MedicalAssistance).IsRequired();
            builder.Property(injurie => injurie.Notes);
            builder.Property(injurie => injurie.CreatedAt).IsRequired();
            builder.Property(injurie => injurie.CreatedById).IsRequired();
            builder.Property(injurie => injurie.LastUpdatedAt).IsRequired();
            builder.Property(injurie => injurie.LastUpdatedById).IsRequired();
        }
    }
}
