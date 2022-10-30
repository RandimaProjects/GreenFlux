using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Models;

namespace SmartCharging.Infrastructure.Context
{
    public class SmartChargingDBContext : DbContext
    {
        public SmartChargingDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Group> Group { get; set; }
        public DbSet<ChargeStation> ChargeStation { get; set; }
        public DbSet<Connector> Connector { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                         .SelectMany(t => t.GetProperties())
                         .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {

                property.SetColumnType("decimal(18,2)");


            }
        }
    }
}
