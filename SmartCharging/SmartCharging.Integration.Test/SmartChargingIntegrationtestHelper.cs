using Microsoft.EntityFrameworkCore;
using SmartCharging.Domain.Models;
using SmartCharging.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Integration.Test
{
    public class SmartChargingIntegrationtestHelper
    {
        public static Microsoft.EntityFrameworkCore.DbContextOptions<SmartChargingDBContext> SmartChargingDbContextOptionsEfCoreInMemory()
        {
            var options = new DbContextOptionsBuilder<SmartChargingDBContext>()
                .UseInMemoryDatabase($"SmartChargingStoreDatabase{Guid.NewGuid()}")
                .Options;

            return options;
        }

        public static async void CreateDataBaseEfCoreInMemory(DbContextOptions<SmartChargingDBContext> options)
        {
            await using (var context = new SmartChargingDBContext(options))
            {
                CreateData(context);
            }
        }

        public static void CleanDataBase(DbContextOptions<SmartChargingDBContext> options)
        {
            using (var context = new SmartChargingDBContext(options))
            {
                foreach (var group in context.Group)
                    context.Group.Remove(group);
                context.SaveChanges();
            }

            using (var context = new SmartChargingDBContext(options))
            {
                foreach (var ct in context.ChargeStation)
                    context.ChargeStation.Remove(ct);
                context.SaveChanges();
            }

            using (var context = new SmartChargingDBContext(options))
            {
                foreach (var connector in context.Connector)
                    context.Connector.Remove(connector);
                context.SaveChanges();
            }
        }

        private static void CreateData(SmartChargingDBContext smartChargingDbContext)
        {
            smartChargingDbContext.Group.Add(new Group { Name = "Group1", Capacity = 100 });
            smartChargingDbContext.Group.Add(new Group { Name = "Group2", Capacity = 50 });

            smartChargingDbContext.ChargeStation.Add(new ChargeStation { Name = "CT1", GroupId = 1 });
            smartChargingDbContext.ChargeStation.Add(new ChargeStation { Name = "CT2", GroupId = 1 });
            smartChargingDbContext.ChargeStation.Add(new ChargeStation { Name = "CT3", GroupId = 2 });

            smartChargingDbContext.SaveChanges();

            smartChargingDbContext.Connector.Add(new Connector { ChargeStationId = 1, MaxCurrent = 20 });
            smartChargingDbContext.Connector.Add(new Connector { ChargeStationId = 1, MaxCurrent = 20 });
            smartChargingDbContext.Connector.Add(new Connector { ChargeStationId = 2, MaxCurrent = 10 });
            smartChargingDbContext.Connector.Add(new Connector { ChargeStationId = 3, MaxCurrent = 5 });

            smartChargingDbContext.SaveChanges();
        }
    }

}
