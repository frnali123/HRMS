using System.ComponentModel.DataAnnotations;

namespace HRMS.Model
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }// employee id from login/session
        public string SenderRole { get; set; } // always "Employee" or "HR"
       
        public string ReceiverRole { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Discription { get; set; }
        public string Attachments { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool IsRead { get; set; } = false; // For notification panel
    }
}

