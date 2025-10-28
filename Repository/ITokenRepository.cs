using HRMS.Model;
using Microsoft.AspNetCore.Identity;

namespace HRMS.Repository
{
    public interface ITokenRepository
    {
        public string CreateJwtToken(User user, Employee employee);
    }
}
