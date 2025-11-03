using static HRMS.Model.DTO.Dashboard.HrDashboardDtos;

namespace HRMS.Services
{
  public interface IhrDahboardService
  {
    Task<HrDashboardDto> GetHrDashboardDataAsync();
  }
}
