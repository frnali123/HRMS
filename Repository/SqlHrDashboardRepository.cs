using HRMS.Database;
using HRMS.Enums;
using HRMS.Model;
using HRMS.Model.DTO.Dashboard;
using Microsoft.EntityFrameworkCore;
using static HRMS.Model.DTO.Dashboard.HrDashboardDtos;
using System.Globalization;

namespace HRMS.Repository
{
  public class SqlHrDashboardRepository : IHrDashboardRepository
  {
    private readonly ApplicationDbContext dbContext;
    public SqlHrDashboardRepository(ApplicationDbContext dbContext)
    {
      this.dbContext = dbContext;
    }
    public async Task<HrAttendanceStats> GetAttendanceStatsAsync(DateTime today)
    {
      var totalEmployees = await GetTotalEmployeeCountAsync();

      var status=await dbContext.Attendances.Where(e=>e.AttendanceDate.Date==today.Date).GroupBy(e=>1).Select(g => new HrAttendanceStats
      {
        TotalPresent = g.Count(a => a.Status == AttendanceStatus.Present),
        AbsentToday = g.Count(a => a.Status == AttendanceStatus.Absent),
        OnLeaveToday = g.Count(a => a.Status == AttendanceStatus.OnLeave),
        RemoteToday = g.Count(a => a.Status == AttendanceStatus.WorkFromHome)
      })
            .FirstOrDefaultAsync();

      if (status == null)
      {
        // Agar aaj ka koi record nahi hai
        return new HrAttendanceStats { TotalEmployees = totalEmployees };
      }

      status.TotalEmployees = totalEmployees;
      return status;
    }


    public async Task<EmployeeDistributionChartDto> GetEmployeeDistributionAsync()
    {
      var totalEmployees = await GetTotalEmployeeCountAsync();
        
        var breakdown = await dbContext.Employees
           .Where(e => e.Status == "IsActive" && e.JobRole != null)
            .GroupBy(e => e.JobRole)
            .Select(g => new { Role = g.Key, Count = g.Count() })
            .ToDictionaryAsync(k => k.Role, v => v.Count);

        return new EmployeeDistributionChartDto
        {
            TotalEmployees = totalEmployees,
            RoleBreakdown = breakdown
        };
    }

    public async Task<List<Employee>> GetEmployeeStatusPreviewAsync(int count)
    {
      return await dbContext.Employees
           
            .Where(e => e.Status=="IsActive")
            .OrderByDescending(e => e.JoiningDate)
            .Take(count)
            .ToListAsync();
    }
  

    public async Task<int> GetTotalEmployeeCountAsync()
    {
      // Sirf active employees ko count karein
      return await dbContext.Employees.CountAsync(e => e.Status == "IsActive");
    }


    public async Task<HrDashboardDtos.WorkHoursChartDto> GetWorkHoursChartDataAsync()
    {
      var today = DateTime.Today;
      var labels = new List<string>();
      var data = new List<double>();

      var cultureInfo = new CultureInfo("en-US");
      var calendar = cultureInfo.Calendar;

      for (int i = 3; i >= 0; i--)
      {
        var weekStartDate = today.AddDays(-(int)today.DayOfWeek - (i * 7));
        var weekEndDate = weekStartDate.AddDays(6);

        // Yahan badlav kiya gaya hai:
        // Hum total minutes ko calculate kar rahe hain
        var totalMinutes = await dbContext.Attendances // Yahan 'AttendanceRecords' ya 'Attendances' hoga
            .Where(a => a.AttendanceDate.Date >= weekStartDate.Date && a.AttendanceDate.Date <= weekEndDate.Date
                        && a.PunchInTime.HasValue // Sirf unko jinoho ne punch-in kiya hai
                        && a.PunchOutTime.HasValue) // Sirf unko jinoho ne punch-out kiya hai
            .SumAsync(a => (double)EF.Functions.DateDiffMinute(a.PunchInTime.Value, a.PunchOutTime.Value));

        // Minutes ko ghanton (hours) mein convert karein
        var totalHours = totalMinutes / 60.0;

        labels.Add($"Week {4 - i}");
        data.Add(totalHours);
      }

      return new WorkHoursChartDto { Labels = labels, Data = data };
    }
  }
}
