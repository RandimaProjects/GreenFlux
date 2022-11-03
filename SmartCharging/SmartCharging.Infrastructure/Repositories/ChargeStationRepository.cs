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
    public class ChargeStationRepository : Repository<ChargeStation>, IChargeStationRepository
    {
        public ChargeStationRepository(SmartChargingDBContext context) : base(context)
        {
        }

        public override async Task<List<ChargeStation>> GetAll()
        {
            return await Db.ChargeStation.AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();
        }

        public override async Task<ChargeStation> GetById(int Id)
        {
            return await Db.ChargeStation.AsNoTracking().Include(c => c.Connectors)
                .Where(g => g.Id == Id)
                .SingleOrDefaultAsync(); ;
        }

        public List<ChargeStation> GetAllWithConnectors(int groupId)
        {
            return Db.ChargeStation.AsNoTracking().Include(c => c.Connectors)
                .Where(g => g.GroupId == groupId)
                .ToList(); ;

        }
        
    }
}
