

using HRMS.Model.DTO.Dashboard;

namespace HRMS.Services
{
  public interface IDashboardService
  {
    Task<EmployeeDashboardDto> GetEmployeeDashboardDataAsync(Guid employeeId);

  }
}
