using HRMS.Model;

namespace HRMS.Repository
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();

        Task<Employee> GetByIdAsync(Guid id);
        Task<Employee> CreateAsync(Employee employee);

        Task<Employee> UpdateAsync(Guid id, Employee employee);
        Task<bool> ExistsAsync(Guid id);

    }
}
