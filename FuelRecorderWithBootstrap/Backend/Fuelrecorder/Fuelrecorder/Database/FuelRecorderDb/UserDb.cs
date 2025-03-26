using System.Data.SqlClient;
using System.Security.Claims;
using System.Text;
using Fuelrecorder.Models;
using Fuelrecorder.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using LoginRequest = Fuelrecorder.Models.LoginRequest;

namespace Fuelrecorder.Database;

public class UserDb
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public UserDb(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IResult> UserLoginRequest([FromBody] LoginRequest loginRequest)
    {
        var user = await GetUser(loginRequest);
        
        if (user == null || 
            !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Results.Problem(
                title: "Unauthorized",
                detail: "Feil brukernavn eller passord",
                statusCode: StatusCodes.Status401Unauthorized);
        }
            
        var access = await GetAccsessInfo(user);
        
        return Results.Ok(access);
    }

    private async Task<User> GetUser(LoginRequest loginRequest)
    {
        User user = null;

        await using var connection = new SqlConnection(_connectionString);
        
        await connection.OpenAsync();

        string sqlQuery = "SELECT Id, Username, PasswordHash FROM Users WHERE Username = @Username";

        await using var command = new SqlCommand(sqlQuery, connection);
        command.Parameters.AddWithValue("@Username", loginRequest.Username);

        await using var reader = await command.ExecuteReaderAsync();
        
            if (await reader.ReadAsync())
            {
                user = new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    PasswordHash = reader.GetString(2),
                };
            }
        
        
        return user;
    }

    private async Task<object> GetAccsessInfo(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return new
        {
            token = tokenString,
            expiration = tokenDescriptor.Expires,
            userId = user.Id,
            userName = user.Username,
        };

    }
    
}