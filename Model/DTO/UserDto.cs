using System.ComponentModel.DataAnnotations;

namespace HRMS.Model.DTO
{
    public class UserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Roles { get; set; }
    }
}
