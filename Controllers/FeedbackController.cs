using AutoMapper;
using HRMS.Model;
using HRMS.Model.DTO;
using HRMS.Repository;
using HRMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService feedbackService;


        public FeedbackController(FeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await feedbackService.GetAllFeedbackAsync();
            return Ok(data);
        }
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitFeedback([FromBody] AddFeedbackDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var feedback = await feedbackService.SubmitFeedbackAsync(dto, User);

            return Ok(feedback);
        }
    }
}