using Fuelrecorder.Database;
using Fuelrecorder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuelrecorder.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserDb _db; 
    
    public UserController(UserDb db)
    {
        _db = db;
    }
    
    [HttpPost("login")]
    public async Task<IResult> UserLogin([FromBody] LoginRequest loginRequest) => 
        await _db.UserLoginRequest(loginRequest);
    
    [HttpPost("registration")]
    public async Task<IResult> UserRegistration([FromBody] LoginRequest loginRequest) => 
        await _db.UserRegistration(loginRequest);
    
    [HttpPut("update"), Authorize]
    public async Task<IResult> UserdataUpdate([FromBody] UserUpdateRequest request) => 
        await _db.UserUpdate(request);
}