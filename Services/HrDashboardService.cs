using HRMS.Model.DTO.Dashboard;
using HRMS.Repository;
using Microsoft.Extensions.Logging;
using static HRMS.Model.DTO.Dashboard.HrDashboardDtos;

namespace HRMS.Services
{
  public class HrDashboardService : IhrDahboardService
  {
    private readonly IHrDashboardRepository hrDashboardRepository;

    public HrDashboardService(IHrDashboardRepository hrDashboardRepository)
    {
      this.hrDashboardRepository = hrDashboardRepository;   
    }
    public async Task<HrDashboardDto> GetHrDashboardDataAsync()
    {
      var today = DateTime.Today;

      // ## FIX: 'await' se mili value ko variable mein store karein ##
      var stats = await hrDashboardRepository.GetAttendanceStatsAsync(today);
      var workHours = await hrDashboardRepository.GetWorkHoursChartDataAsync();
      var distribution = await hrDashboardRepository.GetEmployeeDistributionAsync();
    
      var employees = await hrDashboardRepository.GetEmployeeStatusPreviewAsync(5);

      // 1. Attendance Stats DTO banayein (Yeh code aapka sahi hai)
      var attendanceStatsDto = new List<AttendanceStatDto>
    {
        new AttendanceStatDto {
            Title = "Total Present",
            Count = stats.TotalPresent,
            Total = stats.TotalEmployees,
            Percentage = stats.TotalEmployees > 0 ? (double)stats.TotalPresent / stats.TotalEmployees * 100 : 0
        },
        new AttendanceStatDto {
            Title = "Absent Today",
            Count = stats.AbsentToday,
            Total = stats.TotalEmployees,
            Percentage = stats.TotalEmployees > 0 ? (double)stats.AbsentToday / stats.TotalEmployees * 100 : 0
        },
        new AttendanceStatDto {
            Title = "On Leave Today",
            Count = stats.OnLeaveToday,
            Total = stats.TotalEmployees,
            Percentage = stats.TotalEmployees > 0 ? (double)stats.OnLeaveToday / stats.TotalEmployees * 100 : 0
        },
        new AttendanceStatDto {
            Title = "Remote",
            Count = stats.RemoteToday,
            Total = stats.TotalEmployees,
            Percentage = stats.TotalEmployees > 0 ? (double)stats.RemoteToday / stats.TotalEmployees * 100 : 0
        }
    };

     
      // 3. Employee Status DTO banayein (Ismein maine corrections kiye hain)
      var employeesDto = employees.Select(e => new EmployeeStatusDto
      {
        // e.Id (Database ID) ki jagah e.EmployeeCode (String ID jaise "EMP001") ka istemal karein
        EmployeeId = e.Id,
        Name = e.FullName,
        JobRole = e.JobRole, // Yeh aapke Employee model se aa raha hai
        Status = e.Status == "IsActive" ? "Active" : "Inactive" // Yeh aapke code ke hisaab se hai
      }).ToList();

      // 4. Final HR Dashboard DTO assemble karein
      var dashboardDto = new HrDashboardDto
      {
        AttendanceStats = attendanceStatsDto,
        WorkHoursChart = workHours,
        EmployeeDistribution = distribution,


        EmployeeStatusPreview = employeesDto
      };

      return dashboardDto;
    }


  }
  }

