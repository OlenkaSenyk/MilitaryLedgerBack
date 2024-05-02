using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using File = Domain.Entities.File;

namespace Infrastructure.Configurations
{
    public class PeopleConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(person => person.Id);
            builder.Property(person => person.Id).ValueGeneratedOnAdd();
            builder.Property(person => person.FirstName).IsRequired();
            builder.Property(person => person.LastName).IsRequired();
            builder.Property(person => person.MiddleName).IsRequired();
            builder.Property(person => person.DateOfBirth).IsRequired();
            builder.Property(person => person.Phone).IsRequired();
            builder.Property(person => person.Nationality).HasMaxLength(15).IsRequired();
            builder.Property(person => person.Sex).HasMaxLength(15).IsRequired();
            builder.Property(person => person.MaritalStatus).HasMaxLength(15).IsRequired();
            builder.Property(person => person.Education).IsRequired();
            builder.Property(person => person.EducationLevel).HasMaxLength(15).IsRequired();
            builder.Property(person => person.PublicSpecialty);
            builder.Property(person => person.Workplace).IsRequired();
            builder.Property(person => person.TRSSC).IsRequired();
            builder.Property(person => person.RegistrationDate).IsRequired();
            builder.Property(person => person.DischargeDate);
            builder.Property(person => person.DischargeReason);
            builder.Property(person => person.Signature).IsRequired();
            builder.Property(person => person.NeedMMC).IsRequired();
            builder.Property(person => person.LastMMC);
            builder.Property(person => person.Fine);
            builder.Property(person => person.CreatedAt).IsRequired();
            builder.Property(person => person.CreatedById).IsRequired();
            builder.Property(person => person.LastUpdatedAt).IsRequired();
            builder.Property(person => person.LastUpdatedById).IsRequired();

            builder.HasMany(person => person.Addresses)
                .WithOne()
                .HasForeignKey(addresses => addresses.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(person => person.Awards)
                .WithOne()
                .HasForeignKey(awards => awards.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(person => person.CombatParticipations)
                .WithOne()
                .HasForeignKey(participation => participation.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(person => person.Documents)
                .WithOne()
                .HasForeignKey(documents => documents.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(person => person.Files)
                .WithOne()
                .HasForeignKey<File>(file => file.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(person => person.Injuries)
                .WithOne()
                .HasForeignKey(Injurie => Injurie.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(person => person.MedicalData)
                .WithOne()
                .HasForeignKey<MedicalData>(data => data.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(person => person.Parameter)
                .WithOne()
                .HasForeignKey<Parameter>(parameter => parameter.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(person => person.ServiceHistories)
                .WithOne()
                .HasForeignKey(service => service.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
