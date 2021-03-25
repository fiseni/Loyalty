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
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable(nameof(Contact));

            builder.OwnsOne(x => x.Details, o =>
            {
                o.WithOwner();

                o.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
                o.Property(x => x.LastName).IsRequired().HasMaxLength(100);
                o.Property(x => x.Email).IsRequired().HasMaxLength(250);
                o.Property(x => x.Phone).HasMaxLength(100);
            });

            builder.HasKey(x => x.Id);
        }
    }
}
