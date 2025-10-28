using AutoMapper;
using HRMS.Model.DTO;
using HRMS.Model.HRMS.Models;
using HRMS.Repository;
using HRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AttendanceController : ControllerBase
    {
        private readonly AttendanceService attendanceService;
        public AttendanceController(AttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
    
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await attendanceService.GetAllAttendanceAsync();
            return Ok(data);
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
           var record= await attendanceService.GetAttendanceByIdAsync(id);  
            if(record==null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult>Create(AddAttendanceRequestDto addAttendanceRequestDto)
        {
           var data= await attendanceService.AddAttendanceAsync(addAttendanceRequestDto);
            return Ok(data);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult>Update([FromRoute]Guid id,UpdateAttendanceRequestDto updateAttendanceRequestDto)
        {
                var attendEmployee = await attendanceService.UpdateAttendanceAsync(id, updateAttendanceRequestDto);
                if (attendEmployee == null)
                    return NotFound();

                return Ok(attendEmployee);
            }
        }

    }

