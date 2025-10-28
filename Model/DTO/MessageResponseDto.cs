using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class MessageResponseDto
    {
        public Guid Id { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public string SenderRole { get; set; } // Admin / HR / Employee
       
        public Guid ReceiverId { get; set; }
        [Required]
        public string ReceiverRole { get; set; } // Admin / HR / Employee
        [Required]
        [StringLength(200)]

        public string Subject { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }
        public string Attachments { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
