using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class MessageRequestDto
    {

        [Required]
        public string ReceiverRole { get; set; } // Admin / HR / Employee
        [Required]
        [StringLength(200)]

        public string Subject { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }
        public string Attachments { get; set; }
    }
}
