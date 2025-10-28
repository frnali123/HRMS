using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO.Dashboard
{
  //============== 1. Main Dashboard DTO ==============
  public class EmployeeDashboardDto
  {
    public PersonalDetailsDto PersonalDetails { get; set; }
    public AttendanceSummaryDto AttendanceSummary { get; set; }
    public List<DailyAttendanceDto> Last7DaysAttendance { get; set; }
    public LeaveBalanceDto LeaveBalance { get; set; }
    public SalarySummaryDto SalarySummary { get; set; }
    public List<UpcomingEventDto> UpcomingEvents { get; set; }
    public List<EmployeeDocumentDto> EmployeeDocuments { get; set; }
  }
  //============== 2. Profile Page DTO ==============
  public class PersonalDetailsDto
  {
    public string FullName { get; set; }
    public string JobRole { get; set; }
    public string Department { get; set; }
    public Guid EmployeeId { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime JoiningDate { get; set; }
    public string FatherName { get; set; }
    public string EmergencyContact { get; set; }
    public string Address { get; set; }

  }

  public class EmployeeDocumentDto
  {
  
    public string TenthPassCertificate { get; set; }
    public string TwelfthPassCertificate { get; set; } // "PDF", "DOCX"
    public string GraduationCertificate { get; set; }

  }
  //============== 3. Attendance Page DTO ==============

  public class AttendanceSummaryDto
  {
    public int PresentDays { get; set; }
    public int LeaveDays { get; set; }
    public int TotalWorkingDays { get; set; }
    public int Holidays { get; set; }
    public int AbsentDays { get; set; }
    public string TodaysStatus { get; set; } // e.g., "Punched In", "Not Punched In"
    public string TotalHoursToday { get; set; } // Format as "HH:mm"
  }

  public class DailyAttendanceDto
  {
    public string Day { get; set; } // "Mon", "Tue"
    public string PunchIn { get; set; } // "09:05 AM"
    public string PunchOut { get; set; } // "06:00 PM"
    public decimal Hours { get; set; }
  }



  //============== 4. Leave Page DTO ==============

  public class LeaveBalanceDto
  {
    public int CurrentMonthAvailable { get; set; }
    public int CarriedForward { get; set; }
    public int TotalUsable => CurrentMonthAvailable + CarriedForward;
    public int LeavesTakenThisMonth { get; set; }
    public int BalanceLeft => TotalUsable - LeavesTakenThisMonth;
  }





  //============== 5. Payslip Page DTO ==============
  public class SalarySummaryDto
  {
    public decimal BasicSalary { get; set; }
    public decimal Allowances { get; set; }
    public decimal Deductions { get; set; }
    public decimal NetSalary { get; set; }
  }


  //==============  Upcoming Events ==============
  public class UpcomingEventDto
  {
    public string Title { get; set; }
    public DateTime Time { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
  }

  //============== (NEW) 6. Message & Feedback DTOs ==============




  //============== (NEW) 7. Settings Page DTOs ==============

}

  // Notification preferences GET karne ke liye
 
  // Password change karne ke liye (API POST request ke liye)
  public class ChangePasswordRequestDto
  {
    [Required]
    public string OldPassword { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; }

    [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }


