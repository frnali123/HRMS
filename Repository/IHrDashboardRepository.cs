using HRMS.Model;
using static HRMS.Model.DTO.Dashboard.HrDashboardDtos;

namespace HRMS.Repository
{
  public interface IHrDashboardRepository
  {
    // Yeh ek helper DTO hai jo repository se service tak data laayega
    Task<HrAttendanceStats> GetAttendanceStatsAsync(DateTime today);
    Task<WorkHoursChartDto> GetWorkHoursChartDataAsync();
    Task<EmployeeDistributionChartDto> GetEmployeeDistributionAsync();
   
    Task<List<Employee>> GetEmployeeStatusPreviewAsync(int count);
    Task<int> GetTotalEmployeeCountAsync();
  }
  // Ek helper class jo ek hi query se saare stats laayegi
  public class HrAttendanceStats
  {
    public int TotalPresent { get; set; }
    public int AbsentToday { get; set; }
    public int OnLeaveToday { get; set; }
    public int RemoteToday { get; set; }
    public int TotalEmployees { get; set; }
  }
}
