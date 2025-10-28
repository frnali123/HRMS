using AutoMapper;
using HRMS.Database;
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
    public class EmployeeController : ControllerBase
    {
        
        private readonly EmployeService employeeService;
      
        public EmployeeController( EmployeService employeeService)
        {
        
            this.employeeService = employeeService;
        }

        // Client>>> Dto >>> APi >>> Model >> Database
        //GetAll
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var datamodel = await employeeService.GetAllEmployeeAsync();

            return Ok(datamodel);
        }
       //GetById
        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddEmployeeRequestDto addEmployeeRequestDto)
        {

          var employee=  await employeeService.AddEmployeeAsync(addEmployeeRequestDto);

            return Ok(employee);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, UpdateEmployeeRequestDto dto)
        {
            var updatedEmployee = await employeeService.UpdateEmployeeAsync(id, dto);
            if (updatedEmployee == null)
                return NotFound();

            return Ok(updatedEmployee);
        }



    }
}
