using HRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class AddLeaveRequestDto
    {


        public Guid EmployeeId { get; set; }
        [Required(ErrorMessage = "Leave Type is required")]
     
        public Leavetype LeaveType { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        public string Reason { get; set; }

        [Required]
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending; // Enum: Pending, Approved, Rejected

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string AdditionalCommment { get; set; }
    }
}
