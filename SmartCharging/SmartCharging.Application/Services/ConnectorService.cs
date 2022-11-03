using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Application.Contracts;
using SmartCharging.Application.Contracts.Repository;
using SmartCharging.Application.Contracts.Services;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Services
{
    public class ConnectorService : IConnectorService
    {
        private readonly IConnectorRepository _connectorRepository;

        public ConnectorService(IConnectorRepository connectorRepository)
        {
            _connectorRepository = connectorRepository;
        }

        public async Task<IEnumerable<Connector>> GetAll()
        {
            return await _connectorRepository.GetAll();
        }

        public async Task<Connector> GetById(int id)
        {
            return await _connectorRepository.GetById(id);
        }

        public async Task<Connector> Add(Connector connector)
        {
            //if (_connectorRepository.Search(b => b.Name == group.Name).Result.Any())
            //    return null;

            await _connectorRepository.Add(connector);

            return connector;
        }

        public async Task<Connector> Update(Connector connector)
        {
            //if (_connectorRepository.Search(b => b.Name == group.Name && b.Id != group.Id).Result.Any())
            //    return null;

            await _connectorRepository.Update(connector);
            return connector;
        }

        public async Task<bool> Remove(Connector connector)
        {
            await _connectorRepository.Remove(connector);
            return true;
        }

        public decimal GetTotalMaxCurrent(int groupId)
        {
            return _connectorRepository.GetTotalMaxCurrent(groupId);
        }

        public void Dispose()
        {
            _connectorRepository?.Dispose();
        }
    }
}
