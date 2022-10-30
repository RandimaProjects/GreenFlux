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
    public class ChargeStationMapping : IEntityTypeConfiguration<ChargeStation>
    {
        public void Configure(EntityTypeBuilder<ChargeStation> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasColumnType("varchar(150)");
        }
    }
}
