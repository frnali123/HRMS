using AutoMapper;
using HRMS.Model;
using HRMS.Model.DTO;
using HRMS.Model.HRMS.Models;
using HRMS.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HRMS.Services
{
    public class FeedbackService
    {

        private readonly IFeedbackRepository feedbackRepository;
        private readonly IMapper mapper;

        public FeedbackService(IFeedbackRepository feedbackRepository, IMapper mapper)
        {
            this.feedbackRepository = feedbackRepository;
            this.mapper = mapper;
        }

        //GetAll
        public async Task<List<FeedbackDto>> GetAllFeedbackAsync()
        {
            var feedback = await feedbackRepository.GetAllFeedbacksAsync();
            return mapper.Map<List<FeedbackDto>>(feedback);
        }
        // Create 

        public async Task<FeedbackDto> SubmitFeedbackAsync(AddFeedbackDto dto, ClaimsPrincipal user)
        {
            
            var senderIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (senderIdClaim == null || !Guid.TryParse(senderIdClaim, out var senderId))
            {
                throw new SecurityTokenException("User ID claim not found or invalid in token.");
            }

           
            var senderRole = user.FindFirst(ClaimTypes.Role)?.Value
                             ?? throw new SecurityTokenException("Role claim not found in token.");

           
            var feedback = mapper.Map<Feedback>(dto);
            feedback.SenderId = senderId;         // <-- Set the ID from the token
            feedback.SenderRole = senderRole;       // <-- Set the Role from the token
            feedback.SubmittedAt = DateTime.UtcNow;

            
            var savedFeedback = await feedbackRepository.SubmitFeedbackAsync(feedback);

            return mapper.Map<FeedbackDto>(savedFeedback);
        }
    }

}

