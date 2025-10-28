namespace HRMS.Model.DTO
{
    public class InboxResponseDto
    {
        public int TotalCount { get; set; }
        public int UnreadCount { get; set; }
        public List<MessageResponseDto> Messages { get; set; }
    }
}
