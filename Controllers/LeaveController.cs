using AutoMapper;
using HRMS.Model;
using HRMS.Model.DTO;
using HRMS.Repository;
using HRMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly LeaveService leaveService;
        public LeaveController(LeaveService leaveService)
        {
            this.leaveService = leaveService;
         
        }
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            var data = await leaveService.GetAllleaveByIdAsync();
            return Ok(data);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var data = await leaveService.GetleaveByIdAsync(id);
            if(data==null)
            {
                return NotFound("Id Does not match Current Context");
            }
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult>CreateLeave(AddLeaveRequestDto addLeaveRequestDto)
        {
          
            var leaveCreated = await leaveService.AddleaveAsync(addLeaveRequestDto);
            return Ok(leaveCreated);

        }
        [HttpPut("approve")]
        public async Task<IActionResult> ApproveLeave([FromBody] UpdateLeaveStatusDto dto)
        {
            if (dto == null || dto.LeaveId <= null)
                return BadRequest("Invalid request data.");

            var updatedLeave = await leaveService.UpdateleaveAsync(dto);

            if (updatedLeave == null)
                return NotFound("Leave not found.");

            return Ok(updatedLeave);
        }

    }
}
