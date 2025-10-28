using HRMS.Enums;

namespace HRMS.Model.DTO
{
    public class UpdateAttendanceRequestDto
    {
        public Guid EmployeeId { get; set; }

        public DateTime AttendanceDate { get; set; } = DateTime.UtcNow;

        public DateTime? PunchInTime { get; set; }
        public DateTime? PunchOutTime { get; set; }

        public AttendanceStatus Status { get; set; } // Present, Absent, Late, WFH, On Leave

        public double TotalWorkingHours { get; set; } // in hours

        public DateTime LastSyncTime { get; set; } = DateTime.UtcNow;// last biometric data sync
    }
}
