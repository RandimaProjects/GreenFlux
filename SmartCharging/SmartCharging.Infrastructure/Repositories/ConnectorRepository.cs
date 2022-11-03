using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Application.Contracts.Repository;
using SmartCharging.Domain.Models;
using SmartCharging.Infrastructure.Context;

namespace SmartCharging.Infrastructure.Repositories
{
    public class ConnectorRepository : Repository<Connector>, IConnectorRepository
    {
        public ConnectorRepository(SmartChargingDBContext context) : base(context) { }

        public override async Task<List<Connector>> GetAll()
        {
            return await Db.Connector.AsNoTracking().Include(b => b.ChargeStation)
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        public decimal GetTotalMaxCurrent(int groupId)
        {
            var query =
                from grp in Db.Group   
                join station in Db.ChargeStation  on grp.Id equals station.GroupId
                join cnt in Db.Connector on station.Id equals cnt.ChargeStationId
                where grp.Id == groupId
                group cnt by grp.Id into grpT
                select new { groupId = grpT.Key, Total = grpT.Sum(c => c.MaxCurrent) };
            
            return query.FirstOrDefault().Total;
        }

    }
}
