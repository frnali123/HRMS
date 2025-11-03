using HRMS.Repository;
using HRMS.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class HrDashboardController : ControllerBase
  {
   
    private readonly IhrDahboardService hrdasboardService;

    public HrDashboardController( IhrDahboardService hrdasboardService)
    {
      
      this.hrdasboardService = hrdasboardService;
    }

    // Pehle se bane GetEmployeeDashboard() ke neeche isse add karein

    [HttpGet("hr")]
    [Authorize(Roles = "HR, Admin")] // HR aur Admin dono access kar sakte hain
    public async Task<IActionResult> GetHrDashboard()
    {
      try
      {
        var dashboardData = await hrdasboardService.GetHrDashboardDataAsync();
        return Ok(dashboardData);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Internal server error: {ex.Message}");
      }
    }
  }
 

}
