using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HRMS.Model
{
    public class PerformanceEvaluation
    {
        [Key]
        public Guid EvaluationId { get; set; }

        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        [Required]
        [Range(0, 5)]
        public int WorkQuality { get; set; }

        [Required]
        [Range(0, 5)]
        public int Productivity { get; set; }

        [Required]
        [Range(0, 5)]
        public int Reliability { get; set; }

        [Required]
        [Range(1, 5)]
        public int Innovation { get; set; }

        [Required]
        [Range(0, 5)]
        public int Teamwork { get; set; }

        public double AverageRating { get; set; } // auto-calculated

        [MaxLength(500)]
        public string Comments { get; set; }

        [Required]
        public string EvaluatedBy { get; set; } // HR/Admin Name

        public DateTime EvaluatedOn { get; set; } = DateTime.UtcNow;
    }
}
