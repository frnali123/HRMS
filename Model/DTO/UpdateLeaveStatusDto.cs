using HRMS.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class UpdateLeaveStatusDto
    {
        [Required]
        public Guid LeaveId { get; set; }

        [Required]
        public LeaveStatus Status { get; set; }  // Approved or Rejected
    }
}
