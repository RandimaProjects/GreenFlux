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
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        // GET: api/<Group>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var groups = await _groupService.GetAll();

            return Ok(groups.ToList());
        }

        // POST api/<Group>
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

        // PUT api/<Group>/5
        [HttpPut]
        public async Task<IActionResult> Update(GroupAddDto groupDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                var group = _mapper.Map<Group>(groupDto);
                var result = _groupService.Update(group);
                if (result == null) return BadRequest();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE api/<Group>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id < 1) return BadRequest();
                
                var group = await _groupService.GetById(id);
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
