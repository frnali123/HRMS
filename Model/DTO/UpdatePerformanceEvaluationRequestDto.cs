using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class UpdatePerformanceEvaluationRequestDto
    {

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
    }
}
