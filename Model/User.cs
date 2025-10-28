using System.ComponentModel.DataAnnotations;

namespace HRMS.Model
{
    public class User
    {
        public Guid Id { get; set; }
   
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; }
        [Required]
        public string Roles { get; set; }
    }
}

