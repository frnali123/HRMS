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
    public class MessageController : ControllerBase
    {
        private readonly MessageService messageService;
        public MessageController(MessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var dbdata = await messageService.GetAllMessageAsync();
            return Ok(dbdata);
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] MessageRequestDto messageRequestDto)
        {
           
          var data=  await messageService.SendMessageAsync(messageRequestDto,User);
            return Ok(data);
        }

        [HttpGet]
        [Route("inbox/{reciverid}/{role}")]
        public async Task<IActionResult> GetInbox([FromRoute] Guid reciverid,string role)
        {
            var message = await messageService.GetInboxMessageAsync(reciverid, role);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);

        }
        [HttpGet]
        [Route("sent/{senderid}/{role}")]
        public async Task<IActionResult> GetSent([FromRoute]Guid senderid,string role)
        {
            var message = await messageService.GetSentMessageAsync(senderid, role);
            if(message==null)
            {
                return NotFound();
            }
            return Ok(message);
        }
        [HttpGet]
        [Route("unreadcount/{reciverid}/{role}")]
        public async Task<IActionResult> GetUnreadCount([FromRoute]Guid reciverid,string role)
        {
            var count = await messageService.GetUnreadCount(reciverid, role);
            return Ok(count);
        }
        [HttpPut("markasread/{messageId}")]
        public async Task<ActionResult> MarkAsRead(Guid messageId)
        {
            var updated = await messageService.MarkAsReadMessageAsync(messageId);
            if (!updated) return NotFound();
            return Ok(updated);
        }

    }
}
