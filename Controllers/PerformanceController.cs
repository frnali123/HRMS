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
    public class PerformanceController : ControllerBase
    {
        private readonly PerformanceService performanceService;
        public PerformanceController(PerformanceService performanceService)
        {
            this.performanceService = performanceService;
                
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
           var performance= await performanceService.GetByIdAsync(id);
            return Ok(performance);
        }
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
           var alldata= await performanceService.GetAllAsync();
            return Ok(alldata);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddPerformanceEvaluationRequestDto addPerformanceEvaluation)
        {
         
          var perdata = await performanceService.CreateAsync(addPerformanceEvaluation);
            return Ok(perdata);

        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdatePerformanceEvaluationRequestDto updatePerformance)
        {
          
            var updatedata = await performanceService.UpdateperformanceASync(id, updatePerformance);
            return Ok(updatedata);

        }
    }
}
