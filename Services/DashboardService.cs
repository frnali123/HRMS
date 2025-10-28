
using HRMS.Model.DTO.Dashboard;
using HRMS.Repository;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Services
{
  public class DashboardService : IDashboardService
  {
    private readonly IEmployeeDashboardRepository _employeeDashboardRepository;

    public DashboardService(IEmployeeDashboardRepository employeeDashboardRepository)
    {
      _employeeDashboardRepository = employeeDashboardRepository;
    }
    public async Task<EmployeeDashboardDto?> GetEmployeeDashboardDataAsync(Guid userId)
    {
      // FIX: Replaced 7 repository calls with just ONE.
      // This is now much cleaner and more performant.
      return await _employeeDashboardRepository.GetFullEmployeeDashboardAsync(userId);
    }

    //public async Task<EmployeeDashboardDto> GetEmployeeDashboardDataAsync(Guid employeeId)
    //{

    //  // Pehle personal details nikalein aur check karein
    //  var personalDetails = await _employeeDashboardRepository.GetPersonalDetailsAsync(employeeId);
    //  if (personalDetails == null)
    //  {
    //    return null; // Agar employee hi nahi mila to aage nahi badhna
    //  }

    //  // Ab baki saari details ek-ek karke (sequentially) nikalein
    // var attendanceSummary = await _employeeDashboardRepository.GetAttendanceSummaryAsync(employeeId);
    //  var last7Days = await _employeeDashboardRepository.GetLast7DaysAttendanceAsync(employeeId);
    //  var leaveBalance = await _employeeDashboardRepository.GetLeaveBalanceAsync(employeeId);
    // var salary = await _employeeDashboardRepository.GetLatestSalaryAsync(employeeId);
    //  var events = await _employeeDashboardRepository.GetUpcomingEventsAsync();
    //  var documents = await _employeeDashboardRepository.GetEmployeeDocumentsAsync(employeeId);

    //  // Final DTO ko assemble karein
    //  var dashboardDto = new EmployeeDashboardDto
    //  {
    //    PersonalDetails = personalDetails,
    //    AttendanceSummary = attendanceSummary,
    //    Last7DaysAttendance = last7Days,
    //    LeaveBalance = leaveBalance,
    //    SalarySummary = salary,
    //    UpcomingEvents = events,
    //    EmployeeDocuments = documents
    //  };

    //  return dashboardDto;
    //}
  }
}

