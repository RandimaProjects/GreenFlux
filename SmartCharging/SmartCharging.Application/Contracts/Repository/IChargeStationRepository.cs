using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Contracts.Repository
{
    public interface IChargeStationRepository : IRepository<ChargeStation>
    {
        new Task<List<ChargeStation>> GetAll();
        new Task<ChargeStation> GetById(int id);
        new List<ChargeStation> GetAllWithConnectors(int groupId);
    }
}
