using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace RentYourCar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    // GET
    public IActionResult GetUserId()
    {
        var user = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
        return Ok(user);
    }
}