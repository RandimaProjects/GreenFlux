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
    public class ChargeStationRepository: Repository<ChargeStation>, IChargeStationRepository
    {
        public ChargeStationRepository(SmartChargingDBContext context) : base(context) { }

        public override async Task<List<ChargeStation>> GetAll()
        {
            return await Db.ChargeStation.AsNoTracking().Include(b => b.Group)
                .OrderBy(b => b.Name)
                .ToListAsync();
        }
    }
}
