using HRMS.Database;
using HRMS.Enums;
using HRMS.Model;
using HRMS.Model.DTO.Dashboard;
using HRMS.Model.HRMS.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Repository
{
  public class SqlEmployeeDashboardRepository : IEmployeeDashboardRepository
  {
    private readonly ApplicationDbContext dbContext;
    public SqlEmployeeDashboardRepository(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }


    //FIX: Replaced all individual methods with a single, efficient method.
    public async Task<EmployeeDashboardDto?> GetFullEmployeeDashboardAsync(Guid userId)
    {
      // This single LINQ query fetches ALL data in one database round-trip.
      var dashboardDto = await dbContext.Employees
          .Where(e => e.UserId == userId) // CRITICAL FIX: Query by UserId from the token, not EmployeeId.
          .Select(e => new EmployeeDashboardDto
          {
            // Section 1: Personal Details
            PersonalDetails = new PersonalDetailsDto
            {
              FullName = e.FullName,
              JobRole = e.JobRole,
              Department = e.Department,
              EmployeeId = e.Id, // The actual EmployeeId (PK)
              Email = e.Email,
              PhoneNumber = e.PhoneNumber,
              JoiningDate = e.JoiningDate,
              FatherName = e.FatherName,
              EmergencyContact = e.EmergencyContact,
              Address = e.Address,
            },

            // Section 2: Attendance Summary (Calculated from related Attendance data)
            AttendanceSummary = new AttendanceSummaryDto
            {
              PresentDays = e.Attendances.Count(a => a.AttendanceDate.Month == DateTime.Now.Month && a.Status == AttendanceStatus.Present),
              LeaveDays = e.Attendances.Count(a => a.AttendanceDate.Month == DateTime.Now.Month && a.Status == AttendanceStatus.OnLeave),
              AbsentDays = e.Attendances.Count(a => a.AttendanceDate.Month == DateTime.Now.Month && a.Status == AttendanceStatus.Absent)
            },

            // Section 3: Last 7 Days Attendance
            Last7DaysAttendance = e.Attendances
                  .Where(a => a.AttendanceDate >= DateTime.Now.Date.AddDays(-7))
                  .OrderByDescending(a => a.AttendanceDate)
                  .Select(a => new DailyAttendanceDto
                  {
                    Day = a.AttendanceDate.ToString("ddd"),
                    PunchIn = a.PunchInTime.HasValue ? a.PunchInTime.Value.ToString("hh:mm tt") : "---",
                    PunchOut = a.PunchOutTime.HasValue ? a.PunchOutTime.Value.ToString("hh:mm tt") : "---",
                    Hours = (a.PunchOutTime.HasValue && a.PunchInTime.HasValue) ? (decimal)(a.PunchOutTime.Value - a.PunchInTime.Value).TotalHours : 0
                  }).ToList(),

            // Section 4: Leave Balance (Calculated from related Leave data)
            LeaveBalance = new LeaveBalanceDto
            {
              LeavesTakenThisMonth = e.Leaves.Count(l => l.StartDate.Month == DateTime.Now.Month && l.Status == LeaveStatus.Approved)
              // Add other properties like 'TotalLeaves' if available from the Employee model
            },

            // Section 5: Salary Summary (Directly from Employee table)
            SalarySummary = new SalarySummaryDto
            {
              BasicSalary = e.BasicPay,
              Allowances = e.Allowances,
              Deductions = e.Deductions,
              NetSalary = e.NetSalary
            },

            // Section 6: Documents (Directly from Employee table)
            EmployeeDocuments = new List<EmployeeDocumentDto>
               {
                        new EmployeeDocumentDto
                        {
                             TenthPassCertificate = e.TenthPassCertificate,
                             TwelfthPassCertificate = e.TwelfthPassCertificate,
                             GraduationCertificate = e.GraduationCertificate,
                        }
               }
          })
          .FirstOrDefaultAsync();

      // Section 7: Upcoming Events (This is separate as it's not employee-specific)
      if (dashboardDto != null)
      {
        var today = DateTime.UtcNow.Date;
        dashboardDto.UpcomingEvents = await dbContext.Meetings
            .Where(ev => ev.Date >= today && ev.Date <= today.AddDays(7))
            .OrderBy(ev => ev.Date)
            .Select(ev => new UpcomingEventDto
            {
              Title = ev.Name,
              Time = ev.StartTime,
              Type = ev.Type,
              Date = ev.Date
            }).ToListAsync();
      }

      return dashboardDto;
    }
  }
  //public async Task<PersonalDetailsDto?> GetPersonalDetailsAsync(Guid employeeId)
  //{
  //  try
  //  {
  //    return await dbContext.Employees
  //        .Where(e => e.Id == employeeId)
  //        .Select(e => new PersonalDetailsDto
  //        {
  //          FullName = e.FullName,
  //          JobRole = e.JobRole,
  //          Department = e.Department,
  //          EmployeeId = e.Id,
  //          Email = e.Email,
  //          PhoneNumber = e.PhoneNumber,
  //          JoiningDate = e.JoiningDate,
  //          FatherName = e.FatherName,
  //          EmergencyContact = e.EmergencyContact,
  //          Address = e.Address,
  //        })
  //        .FirstOrDefaultAsync();
  //  }
  //  catch (Exception ex)
  //  {
  //    // Put a breakpoint here and inspect 'ex'
  //    // Or log the full exception details
  //    Console.WriteLine(ex.ToString()); // This will print the full error stack trace
  //    throw; // Re-throw the exception to still return a 500 error
  //  }
  //}
  //// Assuming you have an enum like this somewhere in your project:
  //// public enum AttendanceStatus { Present, Absent, Leave }

//public async Task<AttendanceSummaryDto> GetAttendanceSummaryAsync(Guid employeeId)
//{
//  var currentMonth = DateTime.Now.Month;
//  var currentYear = DateTime.Now.Year;

//  var summary = await dbContext.Attendances
//      .Where(a => a.AttendanceId == employeeId && a.AttendanceDate.Month == currentMonth && a.AttendanceDate.Year == currentYear)
//      .GroupBy(a => a.EmployeeId) // Group by to use aggregate functions
//      .Select(g => new AttendanceSummaryDto
//      {
//        // Compare directly with the enum value. EF Core CAN translate this.
//        PresentDays = g.Count(a => a.Status == AttendanceStatus.Present),
//        LeaveDays = g.Count(a => a.Status == AttendanceStatus.OnLeave),
//        AbsentDays = g.Count(a => a.Status == AttendanceStatus.Absent)
//      })
//      .FirstOrDefaultAsync(); // First, execute the query and get the result

//  // If the result is null (no records found), then return a new empty DTO.
//  return summary ?? new AttendanceSummaryDto();
//}
//public async Task<List<DailyAttendanceDto>> GetLast7DaysAttendanceAsync(Guid employeeId)
//{
//  var dateToCompare = DateTime.Now.Date.AddDays(-7);
//  return await dbContext.Attendances
//      .Where(a => a.AttendanceId == employeeId && a.AttendanceDate >= dateToCompare)
//      .OrderByDescending(a => a.AttendanceDate)
//      .Select(a => new DailyAttendanceDto
//      {
//        Day = a.AttendanceDate.ToString("ddd"),
//        PunchIn = a.PunchInTime.HasValue ? a.PunchInTime.Value.ToString("hh:mm tt") : "---",
//        PunchOut = a.PunchOutTime.HasValue ? a.PunchOutTime.Value.ToString("hh:mm tt") : "---",
//        Hours = (a.PunchOutTime.HasValue && a.PunchInTime.HasValue)
//                  ? (decimal)(a.PunchOutTime.Value - a.PunchInTime.Value).TotalHours
//                  : 0
//      })
//      .Take(7)
//      .ToListAsync();
//}


//public async Task<List<EmployeeDocumentDto>> GetEmployeeDocumentsAsync(Guid employeeId)
//{
//  return await dbContext.Employees
//      .Where(d => d.Id == employeeId)
//      .OrderByDescending(d => d.JoiningDate)
//      .Select(d => new EmployeeDocumentDto
//      {
//        TenthPassCertificate = d.TenthPassCertificate,
//        TwelfthPassCertificate = d.TwelfthPassCertificate,
//        GraduationCertificate = d.GraduationCertificate, // "PDF", "DOCX" etc.

//      })
//      .ToListAsync();
//}
//public async Task<SalarySummaryDto?> GetLatestSalaryAsync(Guid employeeId)
//{
//  return await dbContext.Employees
//      .Where(p => p.Id == employeeId)
//      .OrderByDescending(p => p.JoiningDate.Year)
//      .ThenByDescending(p => p.JoiningDate.Month)
//      .Select(p => new SalarySummaryDto
//      {
//        BasicSalary = p.BasicPay,
//        Allowances = p.Allowances,
//        Deductions = p.Deductions,
//        NetSalary = p.NetSalary
//      })
//      .FirstOrDefaultAsync();
//}

//// Assuming you have an enum like this for leave status:
//// public enum LeaveStatus { Pending, Approved, Rejected }

//public async Task<LeaveBalanceDto> GetLeaveBalanceAsync(Guid employeeId)
//{
//  // Ek hi query me sabkuch get karna
//  var leaveBalance = await dbContext.Leaves
//      .Where(l => l.Id == employeeId) // Assuming 'Id' is the Employee ID in the Leaves table
//      .Select(l => new LeaveBalanceDto
//      {
//        // Calculate the count directly inside the final projection
//        LeavesTakenThisMonth = dbContext.Leaves
//              .Count(subLeave => subLeave.Id == employeeId
//                              && subLeave.Status == LeaveStatus.Approved // <-- FIX #1: Removed .ToString()
//                              && subLeave.StartDate.Month == DateTime.Now.Month
//                              && subLeave.StartDate.Year == DateTime.Now.Year)
//      })
//      .FirstOrDefaultAsync(); // <-- FIX #2: Combined into one query

//  // FIX #3: Handle null case correctly after awaiting
//  return leaveBalance ?? new LeaveBalanceDto();
//}
//public async Task<List<UpcomingEventDto>> GetUpcomingEventsAsync()
//{
//  var today = DateTime.UtcNow.Date;
//  var futureDate = today.AddDays(7); // Agle 7 din ke events

//  return await dbContext.Meetings
//      .Where(e => e.Date >= today && e.Date <= futureDate)
//      .OrderBy(e => e.Date)
//      .ThenBy(e => e.StartTime)
//      .Select(e => new UpcomingEventDto
//      {
//        Title = e.Name,
//        Time = e.StartTime,
//        Type = e.Type,
//        Date = e.Date
//      })
//      .ToListAsync();
//}
}



