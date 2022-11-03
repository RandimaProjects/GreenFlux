using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Contracts.Services
{
    public interface IConnectorService : IDisposable
    {
        Task<IEnumerable<Connector>> GetAll();
        Task<Connector> GetById(int id);
        Task<Connector> Add(Connector connector);
        Task<Connector> Update(Connector connector);
        Task<bool> Remove(Connector connector);
        decimal GetTotalMaxCurrent(int groupId);
    }
}
