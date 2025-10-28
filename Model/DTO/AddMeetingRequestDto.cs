using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class AddMeetingRequestDto
    {
        
        public string Name { get; set; }

   
        public string Type { get; set; }


     
        public DateTime StartTime { get; set; }

     
        public DateTime EndTime { get; set; }
       
        public string Discription { get; set; }
    }
}
