using CustomerApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApp.Infrastructure.DataAccess.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));

            builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).HasMaxLength(250);

            builder.OwnsOne(x => x.Address, o =>
            {
                o.WithOwner();

                o.Property(x => x.Street).IsRequired().HasMaxLength(250);
                o.Property(x => x.City).IsRequired().HasMaxLength(100);
                o.Property(x => x.PostalCode).IsRequired().HasMaxLength(100);
                o.Property(x => x.Country).IsRequired().HasMaxLength(100);
            });

            builder.Metadata.FindNavigation(nameof(Customer.Contacts))
                 .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasKey(x => x.Id);
        }
    }
}
