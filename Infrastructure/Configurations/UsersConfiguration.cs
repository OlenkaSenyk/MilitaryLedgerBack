using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.Id);
            builder.Property(user => user.Id).ValueGeneratedOnAdd();
            builder.Property(user => user.FirstName).HasMaxLength(60).IsRequired();
            builder.Property(user => user.LastName).HasMaxLength(60).IsRequired();
            builder.Property(user => user.MiddleName).HasMaxLength(60).IsRequired();
            builder.Property(user => user.Email).HasMaxLength(100).IsRequired();
            builder.Property(user => user.Password).IsRequired();
            builder.Property(user => user.Phone).HasMaxLength(15);
            builder.Property(user => user.Role).HasMaxLength(15).IsRequired();
            builder.Property(user => user.CreatedAt).IsRequired();
            builder.Property(user => user.CreatedById).IsRequired(false);
            builder.Property(user => user.LastUpdatedAt).IsRequired();
            builder.Property(user => user.LastUpdatedById).IsRequired(false);

            builder.HasMany(user => user.CreatedAddresses)
                .WithOne()
                .HasForeignKey(addresses => addresses.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedAddresses)
                .WithOne()
                .HasForeignKey(addresses => addresses.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedAwards)
                .WithOne()
                .HasForeignKey(awards => awards.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedAwards)
                .WithOne()
                .HasForeignKey(awards => awards.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedCalendarEvents)
                .WithOne()
                .HasForeignKey(calendar => calendar.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedCalendarEvents)
                .WithOne()
                .HasForeignKey(calendar => calendar.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedCombatParticipations)
                .WithOne()
                .HasForeignKey(participation => participation.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedCombatParticipations)
                .WithOne()
                .HasForeignKey(participation => participation.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedDocuments)
                .WithOne()
                .HasForeignKey(document => document.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedDocuments)
                .WithOne()
                .HasForeignKey(document => document.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedDocumentTemplates)
                .WithOne()
                .HasForeignKey(templates => templates.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedDocumentTemplates)
                .WithOne()
                .HasForeignKey(templates => templates.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedFiles)
                .WithOne()
                .HasForeignKey(file => file.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedFiles)
                .WithOne()
                .HasForeignKey(file => file.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedInjuries)
                .WithOne()
                .HasForeignKey(injurie => injurie.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedInjuries)
                .WithOne()
                .HasForeignKey(Injurie => Injurie.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedMedicalDatas)
                .WithOne()
                .HasForeignKey(data => data.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedMedicalDatas)
                .WithOne()
                .HasForeignKey(data => data.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedNews)
                .WithOne()
                .HasForeignKey(news => news.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedNews)
                .WithOne()
                .HasForeignKey(news => news.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedParameters)
                .WithOne()
                .HasForeignKey(parameter => parameter.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedParameters)
                .WithOne()
                .HasForeignKey(parameter => parameter.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedPeople)
                .WithOne()
                .HasForeignKey(people => people.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedPeople)
                .WithOne()
                .HasForeignKey(people => people.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CreatedServiceHistories)
                .WithOne()
                .HasForeignKey(service => service.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.LastUpdatedServiceHistories)
                .WithOne()
                .HasForeignKey(service => service.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.CreatedByUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.LastUpdatedByUser)
                .WithMany()
                .HasForeignKey(u => u.LastUpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.CalendarEvents)
                .WithOne()
                .HasForeignKey(calendar => calendar.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Documents)
                .WithOne()
                .HasForeignKey(document => document.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
