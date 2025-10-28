using HRMS.Model;
using HRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace HRMS.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize(Roles ="Employee")]
  public class EmployeeDashboardController : ControllerBase
  {
    private readonly IDashboardService dashboardService;
    public EmployeeDashboardController(IDashboardService dashboardService)
    {
      this.dashboardService = dashboardService;
    }
    [HttpGet("Employee")] // FIX: More descriptive route
    public async Task<IActionResult> GetEmployeeDashboard()
    {
      // FIX: Cleaner and more direct way to get the user ID claim.
      var userIdClaim = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

      if (!Guid.TryParse(userIdClaim, out var userId))
      {
        // Return a clear error if the token is malformed or the claim is missing.
        return Unauthorized("User ID claim not found or invalid in token.");
      }

      try
      {
        var dashboardData = await dashboardService.GetEmployeeDashboardDataAsync(userId);

        if (dashboardData == null)
          return NotFound("No employee dashboard data found for the logged-in user.");

        return Ok(dashboardData);
      }
      catch (Exception ex)
      {
        // It's a good practice to log the exception 'ex' here
        return StatusCode(500, "An internal server error occurred.");
      }
    }
  }
}
