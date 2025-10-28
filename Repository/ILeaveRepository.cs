using HRMS.Enums;
using HRMS.Model;

namespace HRMS.Repository
{
    public interface ILeaveRepository
    {
        Task<List<Leave>> GetAllLeaveAsync();
        Task<Leave> GetLeaveByIdAsync(Guid id);
        Task<Leave>CreateLeaveAsync(Leave leave);
        Task<Leave>UpdateLeaveStatusAsync(Guid leaveId,LeaveStatus status);
    }
}
