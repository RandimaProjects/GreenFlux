using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Contracts.Services
{
    public interface IChargeStationService : IDisposable
    {
        Task<IEnumerable<ChargeStation>> GetAll();
        Task<ChargeStation> GetById(int id);
        Task<ChargeStation> Add(ChargeStation chargeStation);
        Task<ChargeStation> Update(ChargeStation chargeStation);
        Task<bool> Remove(ChargeStation chargeStation);
    }
}
