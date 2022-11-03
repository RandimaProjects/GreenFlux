using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartCharging.Application.Contracts.Repository;
using SmartCharging.Application.Contracts.Services;
using SmartCharging.Domain.Models;

namespace SmartCharging.Application.Services
{
    public class ChargeStationService : IChargeStationService
    {
        private readonly IChargeStationRepository _chargeStationRepository;

        public ChargeStationService(IChargeStationRepository chargeStationRepository)
        {
            _chargeStationRepository = chargeStationRepository;
        }

        public async Task<IEnumerable<ChargeStation>> GetAll()
        {
            return await _chargeStationRepository.GetAll();
        }

        public List<ChargeStation> GetAllWithConnectors(int groupId)
        {
            return _chargeStationRepository.GetAllWithConnectors(groupId);
        }

        public async Task<ChargeStation> GetById(int id)
        {
            return await _chargeStationRepository.GetById(id);
        }

        public async Task<ChargeStation> Add(ChargeStation chargeStation)
        {
            if (_chargeStationRepository.Search(b => b.Name == chargeStation.Name).Result.Any())
                return null;

            await _chargeStationRepository.Add(chargeStation);

            return chargeStation;
        }

        public async Task<ChargeStation> Update(ChargeStation chargeStation)
        {
            if (_chargeStationRepository.Search(b => b.Name == chargeStation.Name && b.Id != chargeStation.Id).Result.Any())
                return null;

            await _chargeStationRepository.Update(chargeStation);
            return chargeStation;
        }

        public async Task<bool> Remove(ChargeStation chargeStation)
        {
            await _chargeStationRepository.Remove(chargeStation);
            return true;
        }
        public void Dispose()
        {
            _chargeStationRepository?.Dispose();
        }
    }
}
