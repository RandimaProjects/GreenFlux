using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartCharging.Application.Contracts.Services;
using SmartCharging.Domain.Models;
using SmartCharging.Dto;


namespace SmartCharging.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargeStationController : ControllerBase
    {
        private readonly IChargeStationService _chargeStationService;
        private readonly IConnectorService _connectorService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        private const int MAX_CHARGESTATION_COUNT = 5;

        public ChargeStationController(IChargeStationService chargeStationService, IMapper mapper, IGroupService groupService
            , IConnectorService connectorService)
        {
            _chargeStationService = chargeStationService;
            _mapper = mapper;
            _groupService = groupService;
            _connectorService = connectorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var stations = await _chargeStationService.GetAll();
           return Ok(_mapper.Map<IEnumerable<ChargeStationViewDto>>(stations));
        }

        [HttpPost]
        public async Task<IActionResult> Post(ChargeStationAddDto chargeStationDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var group = _groupService.GetById(chargeStationDto.GroupId).Result;
                if(group == null || group.ChargeStations.Count() == MAX_CHARGESTATION_COUNT) return BadRequest("Group charge station count exceeds.");

                if (group.ChargeStations.Any())
                {
                    var totalMaxCurrent = _connectorService.GetTotalMaxCurrent(group.Id);
                    if (group.Capacity < (totalMaxCurrent + chargeStationDto.ConnectorMaxCurrent)) return BadRequest("Exceeds group capacity");
                }
                else
                {
                    if (group.Capacity <  chargeStationDto.ConnectorMaxCurrent) return BadRequest("Exceeds group capacity");
                }

                var chargeStation = new ChargeStation() { GroupId = group.Id, Name = chargeStationDto.Name };
                var reult = await _chargeStationService.Add(chargeStation);

                await _connectorService.Add(new Connector
                    { MaxCurrent = chargeStationDto.ConnectorMaxCurrent, ChargeStationId = reult.Id });
                
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(ChargeStationEditDto chargeStationDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var chargeStation = _chargeStationService.GetById(chargeStationDto.Id).Result;
                if (chargeStation == null) return BadRequest("Charge station not exist");

                chargeStation.Name = chargeStationDto.Name;

                await _chargeStationService.Update(chargeStation);
                

                return Ok();
            }
            catch (Exception ex)
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

                var station = await _chargeStationService.GetById(id);
                if (station == null) return NotFound();

                await _chargeStationService.Remove(station);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
