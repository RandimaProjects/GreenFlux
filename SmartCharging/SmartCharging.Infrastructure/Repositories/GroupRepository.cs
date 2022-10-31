using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartCharging.Application.Contracts;
using SmartCharging.Domain.Models;
using SmartCharging.Infrastructure.Context;

namespace SmartCharging.Infrastructure.Repositories
{
    public  class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(SmartChargingDBContext context) : base(context) { }

        public override async Task<List<Group>> GetAll()
        {
            return await Db.Group.AsNoTracking()
                .OrderBy(b => b.Name)
                .ToListAsync();
        }
    }
}
