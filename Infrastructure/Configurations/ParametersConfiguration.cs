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
    public class ParametersConfiguration : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            builder.ToTable("Parameters");
            builder.HasKey(parameter => parameter.Id);
            builder.Property(parameter => parameter.Id).ValueGeneratedOnAdd();
            builder.Property(parameter => parameter.Height).HasMaxLength(3).IsRequired();
            builder.Property(parameter => parameter.Width).HasMaxLength(3).IsRequired();
            builder.Property(parameter => parameter.ShoeSize).HasMaxLength(2).IsRequired();
            builder.Property(parameter => parameter.ClothingSize).HasMaxLength(10).IsRequired();
            builder.Property(parameter => parameter.GasMaskSize).HasMaxLength(10).IsRequired();
            builder.Property(parameter => parameter.HeadCircumference).HasMaxLength(2).IsRequired();
            builder.Property(parameter => parameter.CreatedAt).IsRequired();
            builder.Property(parameter => parameter .CreatedById).IsRequired();
            builder.Property(parameter => parameter.LastUpdatedAt).IsRequired();
            builder.Property(parameter => parameter.LastUpdatedById).IsRequired();
        }
    }
}
