using HRMS.Model;
using HRMS.Model.DTO.Dashboard;
using HRMS.Model.HRMS.Models;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace HRMS.Repository
{
  public interface IEmployeeDashboardRepository
  {



    Task<EmployeeDashboardDto?> GetFullEmployeeDashboardAsync(Guid userId);
    //Task<PersonalDetailsDto> GetPersonalDetailsAsync(Guid employeeId);
    //Task<AttendanceSummaryDto> GetAttendanceSummaryAsync(Guid employeeId);
    //Task<List<DailyAttendanceDto>> GetLast7DaysAttendanceAsync(Guid employeeId);
    //Task<LeaveBalanceDto> GetLeaveBalanceAsync(Guid employeeId);
    //Task<SalarySummaryDto> GetLatestSalaryAsync(Guid employeeId);
    //Task<List<UpcomingEventDto>> GetUpcomingEventsAsync();
    //Task<List<EmployeeDocumentDto>> GetEmployeeDocumentsAsync(Guid employeeId);





  }
}
