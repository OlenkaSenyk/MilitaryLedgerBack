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
    public class CombatParticipationsConfiguration : IEntityTypeConfiguration<CombatParticipation>
    {
        public void Configure(EntityTypeBuilder<CombatParticipation> builder)
        {
            builder.ToTable("CombatParticipations");
            builder.HasKey(participation => participation.Id);
            builder.Property(participation => participation.Id).ValueGeneratedOnAdd();
            builder.Property(participation => participation.StartDate).IsRequired();
            builder.Property(participation => participation.EndDate).IsRequired();
            builder.Property(participation => participation.Location).HasMaxLength(500).IsRequired();
            builder.Property(participation => participation.OperationType).HasMaxLength(200).IsRequired();
            builder.Property(participation => participation.CreatedAt).IsRequired();
            builder.Property(participation => participation.CreatedById).IsRequired();
            builder.Property(participation => participation.LastUpdatedAt).IsRequired();
            builder.Property(participation => participation.LastUpdatedById).IsRequired();
        }
    }
}
