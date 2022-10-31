using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Contracts.Services
{
    public interface IGroupService : IDisposable
    {
        Task<IEnumerable<Group>> GetAll();
        Task<Group> GetById(int id);
        Task<Group> Add(Group group);
        Task<Group> Update(Group group);
        Task<bool> Remove(Group group);
    }
}
