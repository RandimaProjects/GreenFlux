using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Application.Contracts.Services;
using SmartCharging.Domain.Models;
using SmartCharging.Dto;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartCharging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectorController : ControllerBase
    {
        private readonly IConnectorService _connectorService;
        private readonly IChargeStationService _chargeStationService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public ConnectorController(IConnectorService connectorService, IMapper mapper, IChargeStationService chargeStationService,
            IGroupService groupService)
        {
            _connectorService = connectorService;
            _mapper = mapper;
            _chargeStationService = chargeStationService;
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var connectors = await _connectorService.GetAll();
                return Ok(_mapper.Map<IEnumerable<ConnectorDto>>(connectors));
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConnectorAddDto connectorDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var station = _chargeStationService.GetById(connectorDto.ChargeStationId).Result;
                if (station == null) return BadRequest("Invalid Charge Station Id");

                var group = _groupService.GetById(station.GroupId).Result;
                var totalMaxCurrent = _connectorService.GetTotalMaxCurrent(group.Id);

                if (group.Capacity < (totalMaxCurrent + connectorDto.MaxCurrent)) return BadRequest("Exceeds group capacity");
                
                var connector = _mapper.Map<Connector>(connectorDto);
                await _connectorService.Add(connector);
                

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ConnectorEditDto connectorDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var connector = _connectorService.GetById(connectorDto.Id).Result;
                if (connector == null) return BadRequest();

                var station = _chargeStationService.GetById(connector.ChargeStationId).Result;
                if (station == null) return BadRequest("Invalid Charge Station Id");

                var group = _groupService.GetById(station.GroupId).Result;
                var totalMaxCurrent = _connectorService.GetTotalMaxCurrent(group.Id);

                if (group.Capacity < (totalMaxCurrent + connectorDto.MaxCurrent)) return BadRequest("Exceeds group capacity");


                connector.MaxCurrent = connectorDto.MaxCurrent;
                await _connectorService.Update(connector);

               return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return BadRequest();

                var connector = _connectorService.GetById(id).Result;
                if (connector == null) return NotFound();

                var chargeStation = _chargeStationService.GetById(connector.ChargeStationId).Result;
                if (chargeStation != null && chargeStation.Connectors.Count() == 1) return BadRequest("Charge station must have at least one Connector");
                
                await _connectorService.Remove(connector);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
