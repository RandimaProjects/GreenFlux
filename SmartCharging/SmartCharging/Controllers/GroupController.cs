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
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IChargeStationService _chargeStationService;
        private readonly IConnectorService _connectorService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper, IChargeStationService chargeStationService,
            IConnectorService connectorService)
        {
            _groupService = groupService;
            _mapper = mapper;
            _chargeStationService = chargeStationService;
            _connectorService = connectorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var groups = await _groupService.GetAll();

                return Ok(_mapper.Map<IEnumerable<GroupViewDto>>(groups));
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(GroupAddDto groupDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();
               
                var group = _mapper.Map<Group>(groupDto);
                var result = _groupService.Add(group);
                if (result == null) return BadRequest();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        
        [HttpPut]
        public async Task<IActionResult> Update(GroupEditDto groupDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();


                var group = _groupService.GetById(groupDto.Id).Result;
                if (group == null) return BadRequest("Group not exists");

                if (group.ChargeStations.Any())
                {
                    var totalMaxCurrent = _connectorService.GetTotalMaxCurrent(group.Id);
                    if (group.Capacity < totalMaxCurrent) return BadRequest("Group capacity should be greater than " + totalMaxCurrent);
                }

                group.Name = groupDto.Name;
                group.Capacity = groupDto.Capacity;
                await _groupService.Update(group);

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
                
                var group =  _groupService.GetById(id).Result;
                if (group == null) return NotFound();

                await _groupService.Remove(group);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
