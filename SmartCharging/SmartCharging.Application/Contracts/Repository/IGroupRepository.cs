using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Application.Contracts.Repository;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Contracts
{
    public interface IGroupRepository : IRepository<Group>
    {
        new Task<List<Group>> GetAll();
        new Task<Group> GetById(int id);
    }
}
