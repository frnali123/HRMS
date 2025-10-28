using HRMS.Model.DTO;
using HRMS.Repository;
using HRMS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
    private readonly AuthService authService;

        public AccountController(AuthService authService)
        {
            
            this.authService = authService;
        }
        //[HttpPost]
        //[Route("Register")]
        //public async Task<IActionResult> Register([FromBody] UserDto registerRequestDto)
        //{
        //    var user = await authService.RegisterAync(registerRequestDto);
        //    if (user is null)
        //        return BadRequest("Username already exists!");
        //    return Ok(user);
        //}
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto request)
        {
            var token = await authService.LoginAsync(request);
            if (token is null)
                return BadRequest("Username/Password is wrong!");
            return Ok(token);
        }


        //[HttpPost]
        //[Route("Register")]
        //public async Task<IActionResult> Register([FromBody]UserDto registerRequestDto)
        //{
        //    var User = new IdentityUser
        //    {
        //        UserName = registerRequestDto.UserName,
        //        Email = registerRequestDto.UserName
        //    };
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
        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        //{
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
        //public async Task<IActionResult>Logout()
        //{
        //    await signInManager.SignOutAsync();
        //    return Ok("You have been Logged out Successfully");
        //}
    }
}

