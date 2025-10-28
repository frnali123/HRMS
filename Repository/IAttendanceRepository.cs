using HRMS.Model.HRMS.Models;

namespace HRMS.Repository
{
    public interface IAttendanceRepository
    {
        Task<List<Attendance>> GetAllAsync();
        Task<Attendance> GetByIdAsync(Guid id);
        Task<Attendance>CreateAsync(Attendance attendance);
        Task<Attendance> UpdateAsync(Guid id,Attendance attendance);
    }
}
