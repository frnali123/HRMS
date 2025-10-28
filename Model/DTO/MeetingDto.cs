using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class MeetingDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Meeting name is required")]
        [StringLength(100, ErrorMessage = "Meeting name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Meeting type is required")]
        [StringLength(50, ErrorMessage = "Meeting type cannot exceed 50 characters")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Meeting date is required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Discription is required")]
        [StringLength(500, ErrorMessage = "Meeting Discription cannot exceed 500 characters")]
        public string Discription { get; set; }
    }
}
