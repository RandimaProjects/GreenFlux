using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartCharging.Domain.Models;

namespace SmartCharging.Infrastructure.Mappings
{
    public class ConnectorMapping : IEntityTypeConfiguration<Connector>
    {
        public void Configure(EntityTypeBuilder<Connector> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.MaxCurrent)
                .IsRequired()
                .HasColumnType("decimal");
        }
    }
}
