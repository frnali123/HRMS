using HRMS.Model;
using HRMS.Model.DTO;

namespace HRMS.Repository
{
    public interface IMessageRepository
    {
        Task<List<Message>> GetAllAsync();
        Task<Message> SendMessageAsync(Message message);
        Task<List<Message>> GetInboxAsync(Guid reciverid,string role);
        Task<int> GetInboxCountAsync(Guid reciverid, string role);
        Task<int> GetUnreadCountAsync(Guid reciverid, string role);
        Task<List<Message>>GetSentAsync(Guid senderid,string role);
        Task<bool> MarkAsReadAsync(Guid messageid);
    }
}
