using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class AddFeedbackDto
    {
       
      
        public string ReceiverRole { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string Discription { get; set; }
        public string Attachments { get; set; }
    }
}
