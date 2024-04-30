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
    public class CalendarEventsConfiguration : IEntityTypeConfiguration<CalendarEvent>
    {
        public void Configure(EntityTypeBuilder<CalendarEvent> builder)
        {
            builder.ToTable("CalendarEvents");
            builder.HasKey(calendar => calendar.Id);
            builder.Property(calendar => calendar.Id).ValueGeneratedOnAdd();
            builder.Property(calendar => calendar.Title).HasMaxLength(200).IsRequired();
            builder.Property(calendar => calendar.Type).HasMaxLength(100).IsRequired();
            builder.Property(calendar => calendar.Description);
            builder.Property(calendar => calendar.StartDate).IsRequired();
            builder.Property(calendar => calendar.EndDate).IsRequired();
            builder.Property(calendar => calendar.CreatedAt).IsRequired();
            builder.Property(calendar => calendar.CreatedById).IsRequired();
            builder.Property(calendar => calendar.LastUpdatedAt).IsRequired();
            builder.Property(calendar => calendar.LastUpdatedById).IsRequired();
        }
    }
}
