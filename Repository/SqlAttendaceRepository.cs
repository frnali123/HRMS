using HRMS.Database;
using HRMS.Model.HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Repository
{
    public class SqlAttendaceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext dbContext;
        public SqlAttendaceRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Attendance>CreateAsync(Attendance attendance)
        {
           await dbContext.Attendances.AddAsync(attendance);
            await dbContext.SaveChangesAsync();
            return attendance;

        }

        public async Task<List<Attendance>> GetAllAsync()
        {
            return await dbContext.Attendances.ToListAsync();
        }

        public async Task<Attendance> GetByIdAsync(Guid id)
        {
          return await dbContext.Attendances.FirstOrDefaultAsync(e => e.EmployeeId == id);
            
        }

        public async Task<Attendance> UpdateAsync(Guid id, Attendance attendance)
        {
           var record= await dbContext.Attendances.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if(record==null)
            {
                return null;
            }
            record.AttendanceDate = attendance.AttendanceDate;
            record.PunchInTime = attendance.PunchInTime;
            record.PunchOutTime = attendance.PunchOutTime;
            record.LastSyncTime = attendance.LastSyncTime;
            record.Status = attendance.Status;
            record.TotalWorkingHours=attendance.TotalWorkingHours;

            await dbContext.SaveChangesAsync();
            return record;
        }
       
    }
}
