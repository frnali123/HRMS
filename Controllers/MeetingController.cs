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
    public class MeetingController : ControllerBase
    {
        private readonly MeetingService meetingService;

        public MeetingController(MeetingService meetingService)
        {

            this.meetingService = meetingService;
        }

        // Client>>> Dto >>> APi >>> Model >> Database
        //GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var datamodel = await meetingService.GetAllMeetingAsync();

            return Ok(datamodel);
        }
        //GetById
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var employee = await meetingService.GetMeetingByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMeetingRequestDto addMeetingRequestDto)
        {

            var metting = await meetingService.AddMeetingAsync(addMeetingRequestDto);

            return Ok(metting);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Update(Guid id, UpdateMeetingRequestDto dto)
        {
            var updatemeeting = await meetingService.UpdateMeetingAsync(id, dto);
            if (updatemeeting == null)
                return NotFound();

            return Ok(updatemeeting);
        }
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletemeting = await meetingService.DeleteMeetingAsync(id);
            return Ok(deletemeting);
        }


    }
}
