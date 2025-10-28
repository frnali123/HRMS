using HRMS.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HRMS.Model
{
    public class Leave
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="EmployeeId is required")]
        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage ="Leave Type is required")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Leavetype LeaveType { get; set; }
        [Required(ErrorMessage ="Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        public string Reason { get; set; }

        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending; // Enum: Pending, Approved, Rejected
        public string AdditionalCommment { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        
    }
}
