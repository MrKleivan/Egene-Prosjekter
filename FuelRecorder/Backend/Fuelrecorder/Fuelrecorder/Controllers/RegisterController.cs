using System.Data.SqlClient;
using Fuelrecorder.Models;
using Microsoft.AspNetCore.Mvc;

namespace Fuelrecorder.Controllers;
[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public RegisterController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] User user)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
            
            string chechQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
            using (SqlCommand checkCmd = new SqlCommand(chechQuery, connection))
            {
                checkCmd.Parameters.AddWithValue("@username", user.Username);
                int userCount = (int)await checkCmd.ExecuteScalarAsync();
                if (userCount > 0)
                {
                    return Results.BadRequest("Brukernavn er allerede i bruk");
                }
            }
            
            string insertQuery = "INSERT INTO Users (Username, PasswordHash) VALUES (@username, @passwordHash)";
            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
            {
                insertCmd.Parameters.AddWithValue("@username", user.Username);
                insertCmd.Parameters.AddWithValue("@passwordHash", user.PasswordHash);
                
                int rowsAffected = await insertCmd.ExecuteNonQueryAsync();
                if (rowsAffected > 0 )
                {
                    return Results.Ok("Bruker er blitt registrert");
                }
                else
                {
                    return Results.BadRequest("Feil ved registrering");
                }
            }
            
        }
    }
}