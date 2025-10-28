namespace HRMS.Model
{
    public class AttendanceLog
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; } //FK

        public DateTime ScanTime { get; set; } // every fingerprint scan

        public string DeviceId { get; set; } // which biometric machine
    }
}
