using HRMS.Database;
using HRMS.Enums;
using HRMS.Model;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Repository
{
    public class SqlLeaveRepository : ILeaveRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlLeaveRepository(ApplicationDbContext dbContext)
        {
           this.dbContext = dbContext;   
        }
        public async Task<Leave> CreateLeaveAsync(Leave leave)
        {
            await dbContext.Leaves.AddAsync(leave);
            await dbContext.SaveChangesAsync();
            return leave;
        }

        public async Task<List<Leave>> GetAllLeaveAsync()
        {
            return await dbContext.Leaves.ToListAsync();
        }

        public async Task<Leave> GetLeaveByIdAsync(Guid id)
        {
            return await dbContext.Leaves.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Leave> UpdateLeaveStatusAsync(Guid leaveId, LeaveStatus status)
        {
            var data = await dbContext.Leaves.FirstOrDefaultAsync(e => e.Id == leaveId);
            if(data==null)
            {
                return null;
            }
            data.Status =status;
            await dbContext.SaveChangesAsync();
            return data;
          
        }
    }
}
