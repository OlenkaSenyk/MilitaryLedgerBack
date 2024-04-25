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
    public class AddressesConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(address => address.Id);
            builder.Property(address => address.Id).ValueGeneratedOnAdd();
            builder.Property(address => address.Country).HasMaxLength(50).IsRequired();
            builder.Property(address => address.City).HasMaxLength(100).IsRequired();
            builder.Property(address => address.Region).HasMaxLength(100);
            builder.Property(address => address.Street).HasMaxLength(100).IsRequired();
            builder.Property(address => address.House).HasMaxLength(10).IsRequired();
            builder.Property(address => address.Entrance).HasMaxLength(10);
            builder.Property(address => address.Apartment).HasMaxLength(10);
            builder.Property(address => address.ResidenceOrRegistration).HasMaxLength(20).IsRequired();
        }
    }
}
