using HRMS.Database;
using HRMS.Model;
using HRMS.Model.DTO;
using HRMS.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace HRMS.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext dbContext;

        private readonly ITokenRepository tokenRepository;

        public AuthService(ITokenRepository tokenRepository, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.tokenRepository = tokenRepository;

        }

        public async Task<User> RegisterAync(UserDto request)
        {
            if (await dbContext.Users.AnyAsync(u => u.Username == request.Username))
                return null;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                PasswordHash = new PasswordHasher<User>().HashPassword(null!, request.Password),
                Roles=request.Roles
               
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }
        //        var user = new User
        //        {
        //            Username = request.Username,
        //            Email = registerRequestDto.UserName
        //        };
        //    var Result = await userManager.CreateAsync(User, registerRequestDto.Password);
        //    if (Result.Succeeded)
        //    {
        //        if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
        //        {
        //            //Add to Role
        //            Result = await userManager.AddToRolesAsync(User, registerRequestDto.Roles);
        //            if (Result.Succeeded)
        //            {
        //                return Ok("The User is Register Plsease Login");
        //            }
        //        }
        //    }
        //    return BadRequest("Something went wrong");
        //}

        public async Task<string?> LoginAsync(UserDto request)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) return null;

            var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (result == PasswordVerificationResult.Failed)
                return null;

      // Pass roles from the user into the token
      //var token=tokenRepository.CreateJwtToken(user);
      // return token;
      // 3. --- YAHAN FIX KAREN ---
      //    User.Id का उपयोग करके संबंधित Employee को ढूँढें
      var employee = await dbContext.Employees
                              .FirstOrDefaultAsync(e => e.UserId == user.Id);

      // 4. अगर User है पर Employee प्रोफाइल नहीं (कभी-कभी हो सकता है)
      if (employee == null)
      {
        // या तो एरर दें या null रिटर्न करें
        // इस केस में, हम लॉगिन को फेल कर देंगे
        return null;
      }

      // 5. टोकन बनाते समय 'User' और 'Employee' दोनों को पास करें
      var token = tokenRepository.CreateJwtToken(user, employee);
      return token;
    }
        //    var user = await userManager.FindByEmailAsync(loginRequestDto.UserName);
        //    if (user != null)
        //    {
        //        var result = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
        //        if (result)
        //        {
        //            //create Token
        //            var roles = await userManager.GetRolesAsync(user);
        //            if (roles != null)
        //            {
        //                var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());

        //                var response = new LoginResponseDto
        //                {
        //                    JwtToken = jwtToken
        //                };
        //                return Ok(response);
        //            }
        //        }
        //        return Ok("Username or password is wrong");
        //    }
        //    return Ok("Username or password is wrong");
        //}
        //[HttpPost]
        //[Route("Logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    await signInManager.SignOutAsync();
        //    return Ok("You have been Logged out Successfully");
        //}
    }
    }
