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
            builder.Property(address => address.Country).IsRequired();
            builder.Property(address => address.City).IsRequired();
            builder.Property(address => address.Region);
            builder.Property(address => address.Street).IsRequired();
            builder.Property(address => address.House).IsRequired();
            builder.Property(address => address.Entrance);
            builder.Property(address => address.Apartment);
            builder.Property(address => address.ResidenceOrRegistration).IsRequired();
            builder.Property(address => address.CreatedAt).IsRequired();
            builder.Property(address => address.CreatedById).IsRequired();
            builder.Property(address => address.LastUpdatedAt).IsRequired();
            builder.Property(address => address.LastUpdatedById).IsRequired();
        }
    }
}
