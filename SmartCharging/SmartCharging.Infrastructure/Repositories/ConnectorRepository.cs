using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

    }
}
