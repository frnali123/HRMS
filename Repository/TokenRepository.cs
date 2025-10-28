using HRMS.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRMS.Repository
{
    public class TokenRepository : ITokenRepository
    {
       
             private IConfiguration configuration { get; }

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string CreateJwtToken(User user,Employee employee)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Roles)
            // new Claim("EmployeeId", user.Id.ToString()) // custom claim
             // --- YEH HAI ASLI FIX ---
            // "EmployeeId" क्लेम में employee.Id (Employee का PK) डालें
            ,new Claim("EmployeeId", employee.Id.ToString())
        };
           

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credantial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credantial

                );
            return new JwtSecurityTokenHandler().WriteToken(token); //create new token
        }
    }
    }

