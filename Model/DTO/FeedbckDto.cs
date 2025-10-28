using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class FeedbackDto
    {
           public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public string SenderRole { get; set; }
      

        // Remove ReciverId
        public string ReceiverRole { get; set; }  // e.g., "HR" or "Admin"

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }
        public string Attachments { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
    }
}
