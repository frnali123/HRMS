using HRMS.Database;
using HRMS.Model;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Repository
{
    public class SqlMeetingRepository : IMeetingRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SqlMeetingRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Meeting> CreateAsync(Meeting meeting)
        {
            await dbContext.Meetings.AddAsync(meeting);
            await dbContext.SaveChangesAsync();
            return meeting;
        }

        public async Task<Meeting> DeleteAsync(Guid id)
        {
            var data = await dbContext.Meetings.FirstOrDefaultAsync(e=>e.Id==id);
            if(data==null)
            {
                return null;
            }
             dbContext.Meetings.Remove(data);
            await dbContext.SaveChangesAsync();
            return data;
        }

        public async Task<List<Meeting>> GetAllAsync()
        {
            return await dbContext.Meetings.ToListAsync();
        }

        public async Task<Meeting> GetByIdAsync(Guid id)
        {
            return await dbContext.Meetings.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Meeting> UpdateAsync(Guid id, Meeting meeting)
        {
           var record= await dbContext.Meetings.FirstOrDefaultAsync(e => e.Id == id);
            if(record==null)
            {
                return null;
            }
            record.Name = meeting.Name;
            record.Type = meeting.Type;
            record.Date = meeting.Date;
            record.StartTime = meeting.StartTime;
            record.EndTime = meeting.EndTime;
            record.EndTime = meeting.EndTime;
            record.Discription = meeting.Discription;
            await dbContext.SaveChangesAsync();
            return record;
        }
    }
}
