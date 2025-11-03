namespace HRMS.Model.DTO.Dashboard
{
  public class HrDashboardDtos
  {
    // Yeh main DTO hai jo HR Dashboard ke saare data ko hold karega.

    public class HrDashboardDto
    {
      public List<AttendanceStatDto> AttendanceStats { get; set; }
      public WorkHoursChartDto WorkHoursChart { get; set; }
      public EmployeeDistributionChartDto EmployeeDistribution { get; set; }
 
      public List<EmployeeStatusDto> EmployeeStatusPreview { get; set; }
    }
    // Top cards ke liye (Total Present, Absent, etc.)
    public class AttendanceStatDto
    {
      public string Title { get; set; }
      public int Count { get; set; }
      public int Total { get; set; }
      public double Percentage { get; set; }
    }

    // Working hours bar chart ke liye
    public class WorkHoursChartDto
    {
      public List<string> Labels { get; set; } // e.g., ["Week 1", "Week 2"]
      public List<double> Data { get; set; }   // e.g., [158.5, 160.2]
    }


    // Employee distribution donut chart ke liye
    public class EmployeeDistributionChartDto
    {
      public int TotalEmployees { get; set; }
      // Key = Role Name (e.g., "Software Engineer"), Value = Count
      public Dictionary<string, int> RoleBreakdown { get; set; }
    }

    // Dashboard par dikhne waali short employee list

    public class EmployeeStatusDto
    {
      public Guid EmployeeId { get; set; }
      public string Name { get; set; }
      public string JobRole { get; set; }
      public string Status { get; set; } // e.g., "Active", "Inactive"
    }
    // Events list ke liye (Yeh Employee Dashboard se reuse kiya ja sakta hai)
   
  }
}
