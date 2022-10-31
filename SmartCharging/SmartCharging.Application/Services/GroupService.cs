using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Application.Contracts;
using SmartCharging.Application.Contracts.Services;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;

        public GroupService(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<IEnumerable<Group>> GetAll()
        {
            return await _groupRepository.GetAll();
        }

        public async Task<Group> GetById(int id)
        {
            return await _groupRepository.GetById(id);
        }

        public async Task<Group> Add(Group group)
        {
            if (_groupRepository.Search(b => b.Name == group.Name).Result.Any())
                return null;

            await _groupRepository.Add(group);
            
            return group;
        }

        public async Task<Group> Update(Group group)
        {
            if (_groupRepository.Search(b => b.Name == group.Name && b.Id != group.Id).Result.Any())
                return null;

            await _groupRepository.Update(group);
            return group;
        }

        public async Task<bool> Remove(Group group)
        {
            await _groupRepository.Remove(group);
            return true;
        }
        public void Dispose()
        {
            _groupRepository?.Dispose();
        }
    }
}
