using HRMS.Database;
using HRMS.Model;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HRMS.Repository
{
    public class SqlMessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlMessageRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Message> SendMessageAsync(Message message)
        {
            await dbContext.Messages.AddAsync(message);
            await dbContext.SaveChangesAsync();
            return message;
        }
        public async Task<List<Message>> GetInboxAsync(Guid reciverid, string role)
        {
            return await dbContext.Messages.Where(e => e.ReceiverId == reciverid && e.ReceiverRole==role).OrderByDescending(e => e.SentAt).ToListAsync();
        }

        public async Task<int> GetInboxCountAsync(Guid reciverid, string role)
        {
            return await dbContext.Messages.CountAsync(e => e.ReceiverId == reciverid && e.ReceiverRole==role);
        }

        public async Task<List<Message>> GetSentAsync(Guid senderid, string role)
        {
            return await dbContext.Messages.Where(e => e.SenderId == senderid && e.SenderRole==role).OrderByDescending(e => e.SentAt).ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(Guid reciverid,string role)
        {
            return await dbContext.Messages
                 .CountAsync(m => m.ReceiverId == reciverid && m.ReceiverRole == role && !m.IsRead);
        }

        public async Task<bool> MarkAsReadAsync(Guid messageid)
        {
            var msg = await dbContext.Messages.FindAsync(messageid);
            if (msg == null) return false;

            msg.IsRead = true;
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Message>> GetAllAsync()
        {
            return await dbContext.Messages.ToListAsync();   
        }

    }
}
