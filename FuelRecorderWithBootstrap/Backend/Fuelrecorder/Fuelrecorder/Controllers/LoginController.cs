using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fuelrecorder.Database;
using Fuelrecorder.Models;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = Fuelrecorder.Models.LoginRequest;

namespace Fuelrecorder.Controllers;
[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    private readonly UserDb _db; 
    
    public LoginController(UserDb db)
    {
        _db = db;
    }
    
    [HttpPost("login")]
    public async Task<IResult> UserLogin([FromBody] LoginRequest loginRequest) => 
        await _db.UserLoginRequest(loginRequest); 
        
    
    
}