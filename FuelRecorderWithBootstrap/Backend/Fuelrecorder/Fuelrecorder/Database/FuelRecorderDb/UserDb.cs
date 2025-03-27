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
        User user = new User();
        var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        
        user = await GetUser(loginRequest.Username, conn);
        
        if (user == null || 
            !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
        {
            return Results.Problem(
                title: "Unauthorized",
                detail: "Feil brukernavn eller passord",
                statusCode: StatusCodes.Status401Unauthorized);
        }
            
        var accsessInfo = await GetAccsessInfo(user);
        
        conn.Close();
        
        return Results.Ok(accsessInfo);
    }
    
    public async Task<IResult> UserRegistration([FromBody] LoginRequest loginRequest)
    {
        User user = null;
        var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        
        bool userExist = await CheckExistence(loginRequest.Username,  conn);

        if (userExist)
        { 
            return Results.BadRequest("Brukernavn er allerede i bruk");
        }
        
        user = await AddUserToDatabase(loginRequest, conn);
        
        conn.Close();

        if (user == null)
        {
            return Results.BadRequest("Feil ved registrering");
        }

        return Results.Ok("Bruker er blitt registrert" + user);
    }
    
    public async Task<IResult> UserUpdate([FromBody] UserUpdateRequest request)
    {
        User user = null;
        

        if (request.NewPassword != "")
        {
            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                return Results.BadRequest("Nytt passord må oppgis.");
            }
        }
    
        var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        
        bool userExist = await CheckExistence(request.OldUserName,  conn);

        if (!userExist)
        {
            return Results.BadRequest("Fant ikke noen bruker ved det navnet(se at gammelt brukernavn er riktig)");
        }
    
        User oldUserInfo = await GetUser(request.OldUserName, conn);

        if (oldUserInfo == null)
        {
            return Results.NotFound("Bruker ikke funnet");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.CurrentPassword);
        
        if (hashedPassword != oldUserInfo.PasswordHash)
        {
            return Results.BadRequest("Feil passord");
        }

        user = await UpdateUserDataToDatabase(request, conn);
        
        if (user == null)
        {
            return Results.BadRequest("Ingen endringer ble gjort.");
        } 
        return Results.Ok("Bruker er oppdatert.");
        
        
    }


    private async Task<User> GetUser(string userName, SqlConnection conn)
    {
        User user = null;
        bool userExist = await CheckExistence(userName, conn);
        if (!userExist)
        {
            return user;
        }
        
        string query = "SELECT Id, Username, PasswordHash FROM Users WHERE Username = @Username";

        var sql = new SqlCommand(query, conn);
        sql.Parameters.AddWithValue("@Username", userName);

        var reader = await sql.ExecuteReaderAsync();
        
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

    private async Task<User> AddUserToDatabase(LoginRequest loginRequest, SqlConnection conn)
    {
        User user = null;
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(loginRequest.Password);
            
        string query = "INSERT INTO Users (Username, PasswordHash) VALUES (@username, @passwordHash)";
        SqlCommand insertCmd = new SqlCommand(query, conn);
        
        insertCmd.Parameters.AddWithValue("@username", loginRequest.Username);
        insertCmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
        
        int rowsAffected = await insertCmd.ExecuteNonQueryAsync();

        if (rowsAffected > 0)
        {
            user = await GetUser(loginRequest.Username, conn);
        }
        
        return user;
    }

    private async Task<User> UpdateUserDataToDatabase(UserUpdateRequest request, SqlConnection conn)
    {
        User user = null;
        string query = "";
        string currentPasswordHash = "";

        if (currentPasswordHash == request.NewPassword || request.NewPassword.Length < 2)
        {
            query = "UPDATE Users SET UserName = @Username WHERE Id = @id";
        } 
        else if (currentPasswordHash != request.NewPassword && request.NewPassword.Length > 2)
        {
            query = "UPDATE Users SET UserName = @Username, PasswordHash = @PasswordHash WHERE Id = @id";
        }
        
        SqlCommand updateCmd = new SqlCommand(query, conn);
        
        updateCmd.Parameters.AddWithValue("@id", request.Id);
        updateCmd.Parameters.AddWithValue("@Username", request.NewUserName);
        updateCmd.Parameters.AddWithValue("@PasswordHash", request.NewPassword);
        
        int rowsAffected = await updateCmd.ExecuteNonQueryAsync();

        if (rowsAffected > 0)
        {
            user = await GetUser(request.NewUserName, conn);
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

    private async Task<bool> CheckExistence(string userName, SqlConnection connection)
    {
        string query = "SELECT COUNT(*) FROM Users WHERE Username = @username";
    
        SqlCommand sql = new SqlCommand(query, connection);
        sql.Parameters.AddWithValue("@username", userName);
        
        int userCount = (int)await sql.ExecuteScalarAsync();
        if (userCount > 0)
        {
            return true;
        }
        return false;
    }
    
}