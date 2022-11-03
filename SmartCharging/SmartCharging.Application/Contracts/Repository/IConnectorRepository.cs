using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Contracts.Repository
{
    public interface IConnectorRepository : IRepository<Connector>
    {
        new Task<List<Connector>> GetAll();
        new Task<Connector> GetById(int id);
        decimal GetTotalMaxCurrent(int groupId);
    }
}
