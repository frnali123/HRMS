using AutoMapper;
using HRMS.Model.DTO;
using HRMS.Model;
using HRMS.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace HRMS.Services
{
    public class MessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IMapper mapper;

        public MessageService(IMessageRepository messageRepository, IMapper mapper)
        {
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }


        public async Task<List<MessageResponseDto>> GetAllMessageAsync()
        {
            var dbdata = await messageRepository.GetAllAsync();
            return mapper.Map<List<MessageResponseDto>>(dbdata);
        }
    
        public async Task<MessageResponseDto> SendMessageAsync(MessageRequestDto messageRequestDto,ClaimsPrincipal user)
        {

            var senderIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (senderIdClaim == null || !Guid.TryParse(senderIdClaim, out var senderId))
            {
              
                throw new SecurityTokenException("User ID claim not found or invalid in token.");
            }
            
            var senderRole = user.FindFirst(ClaimTypes.Role)?.Value
                             ?? throw new SecurityTokenException("Role claim not found in token.");

            
            var message = mapper.Map<Message>(messageRequestDto);
            message.SenderId = senderId;         // <-- Set the ID from the token
            message.SenderRole = senderRole;       // <-- Set the Role from the token
            message.SentAt = DateTime.UtcNow;
            
           var data= await messageRepository.SendMessageAsync(message);
            return mapper.Map<MessageResponseDto>(data);
        }

    
     
        public async Task<List<MessageResponseDto?>> GetInboxMessageAsync(Guid reciverid, string role)
        {
            var message = await messageRepository.GetInboxAsync(reciverid, role);
            if (message == null)
            {
                return null;
            }
            return mapper.Map<List<MessageResponseDto?>>(message);

        }
    
        public async Task<List<MessageResponseDto>> GetSentMessageAsync(Guid senderid, string role)
        {
            var message = await messageRepository.GetSentAsync(senderid, role);
            if (message == null)
            {
                return null;
            }
            return mapper.Map<List<MessageResponseDto>>(message);
        }
      
        public async Task<int> GetUnreadCount(Guid reciverid, string role)
        {
            var count = await messageRepository.GetUnreadCountAsync(reciverid, role);
            return count;
        }
       
        public async Task<bool> MarkAsReadMessageAsync(Guid messageId)
        {
            var updated = await messageRepository.MarkAsReadAsync(messageId);
            if (updated == null)
            {
                return false;
            }
            return true;
        }

    }
}
