using System.Data.SqlClient;
using Fuelrecorder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            
            string insertQuery = "INSERT INTO Users (Username, PasswordHash) VALUES (@username, @passwordHash)";
            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
            {
                insertCmd.Parameters.AddWithValue("@username", user.Username);
                insertCmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
                
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
    
    [HttpPut("update"), Authorize]
    public async Task<IResult> Update([FromBody] UserUpdateRequest request)
    {
        string connectionString = _configuration.GetConnectionString("DefaultConnection");
        string currentPasswordHash = "";

        if (request.NewPassword != "")
        {
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return Results.BadRequest("Nytt passord må oppgis.");
            }
        }
    
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            await connection.OpenAsync();
    
            // Sjekk om brukeren eksisterer og hent passordhash
            string checkQuery = "SELECT UserName, PasswordHash FROM Users WHERE Id = @id";
            using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
            {
                checkCmd.Parameters.AddWithValue("@id", request.Id);
    
                using (SqlDataReader reader = await checkCmd.ExecuteReaderAsync())
                {
                    if (!reader.HasRows)
                    {
                        return Results.NotFound("Bruker ikke funnet");
                    }
    
                    await reader.ReadAsync();
                    currentPasswordHash = reader["PasswordHash"].ToString();
    
                    // Sjekk om det nåværende passordet er korrekt
                    if (currentPasswordHash != request.CurrentPassword)
                    {
                        return Results.BadRequest("Feil passord");
                    }
                }
            }

            string updateQuery = "";

            if (currentPasswordHash == request.NewPassword || request.NewPassword.Length < 2)
            {
                updateQuery = "UPDATE Users SET UserName = @Username WHERE Id = @id";
            } 
            else if (currentPasswordHash != request.NewPassword && request.NewPassword.Length > 2)
            {
                updateQuery = "UPDATE Users SET UserName = @Username, PasswordHash = @PasswordHash WHERE Id = @id";
            }
    
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, connection))
            {
                updateCmd.Parameters.AddWithValue("@id", request.Id);
                updateCmd.Parameters.AddWithValue("@Username", request.NewUsername);
                updateCmd.Parameters.AddWithValue("@PasswordHash", request.NewPassword);
    
                int rowsAffected = await updateCmd.ExecuteNonQueryAsync();
    
                if (rowsAffected > 0)
                {
                    return Results.Ok("Bruker er oppdatert.");
                }
                else
                {
                    return Results.BadRequest("Ingen endringer ble gjort.");
                }
            }
        }
    }

}