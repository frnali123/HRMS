using HRMS.Model;

namespace HRMS.Repository
{
    public interface IMeetingRepository
    {
        Task<List<Meeting>> GetAllAsync();
        Task<Meeting> GetByIdAsync(Guid id);
        Task<Meeting>CreateAsync(Meeting meeting);
        Task<Meeting> UpdateAsync(Guid id, Meeting meeting);
        Task<Meeting>DeleteAsync(Guid id);
    }
}
