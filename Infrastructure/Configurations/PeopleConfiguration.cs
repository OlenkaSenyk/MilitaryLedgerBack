using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Configurations
{
    public class PeopleConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");
            builder.HasKey(person => person.Id);
            builder.Property(address => address.Id).ValueGeneratedOnAdd();
            builder.Property(person => person.FirstName).HasMaxLength(60).IsRequired();
            builder.Property(person => person.LastName).HasMaxLength(60).IsRequired();
            builder.Property(person => person.MiddleName).HasMaxLength(60).IsRequired();
            builder.Property(person => person.DateOfBirth).IsRequired();
            builder.Property(person => person.Phone).HasMaxLength(15).IsRequired();
            builder.Property(person => person.Nationality).HasMaxLength(15).IsRequired();
            builder.Property(person => person.Sex).HasMaxLength(15).IsRequired();
            builder.Property(person => person.MaritalStatus).HasMaxLength(10).IsRequired();
            builder.Property(person => person.Education).IsRequired();
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

            builder.HasMany(person => person.Addresses)
                .WithOne()
                .HasForeignKey(addresses => addresses.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
