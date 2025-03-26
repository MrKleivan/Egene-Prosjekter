using System.Data.SqlClient;
using Fuelrecorder.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Fuelrecorder.Database;

public class FuelRecords
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public FuelRecords(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IResult> GetAll()
    {
        var fuelRecorders = new List<FuelRecorderModel>();
        var conn = new SqlConnection(_connectionString);
        
        await conn.OpenAsync();
        var query = "SELECT * FROM FuelRecords";
        var sql = new SqlCommand(query, conn);

        fuelRecorders = await SetValuesFromQuery(sql);
        
        conn.Close();

        if (fuelRecorders.Count < 1)
        {
            return Results.NotFound();
        }
        return Results.Ok(fuelRecorders);
    }
    
    public async Task<IResult> GetAllFuelRecordsFromUserByUserId(int id)
    {
        var fuelRecorders = new List<FuelRecorderModel>();
        var conn = new SqlConnection(_connectionString);
        
        await conn.OpenAsync();
        var query = "SELECT * FROM FuelRecords WHERE userId = @userId";
        var sql = new SqlCommand(query, conn);
        sql.Parameters.AddWithValue("@userId", id);
        
        fuelRecorders = await SetValuesFromQuery(sql);
        
        conn.Close();
        
        if (fuelRecorders.Count == 0)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(fuelRecorders);
    }
    
    public async Task<IResult> GetAllFuelRecordsFromUserByVehicleId(int id)
    {
        var fuelRecorders = new List<FuelRecorderModel>();
        var conn = new SqlConnection(_connectionString);
        
        await conn.OpenAsync();
        var sql = new SqlCommand("SELECT * FROM FuelRecords WHERE VehicleId = @id", conn);
        sql.Parameters.AddWithValue("@id", id);
        
        fuelRecorders = await SetValuesFromQuery(sql);
        
        conn.Close();
        
        if (fuelRecorders.Count == 0)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(fuelRecorders);
    }

    public async Task<IResult> AddFuelRecord(FuelRecorderModel fuelRecorderModel)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
        
            var query = @"
            INSERT INTO FuelRecords (Kilometer, FuelFilled, Price, UserId, VehicleId)
            VALUES (@kilometer, @fuelFilled, @price, @userId, @vehicleId);
            SELECT SCOPE_IDENTITY();"; // 🔹 Returnerer ID-en til den siste innsettingen

            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@kilometer", fuelRecorderModel.Kilometer);
                command.Parameters.AddWithValue("@fuelFilled", fuelRecorderModel.FuelFilled);
                command.Parameters.AddWithValue("@price", fuelRecorderModel.Price);
                command.Parameters.AddWithValue("@userId", fuelRecorderModel.UserId);
                command.Parameters.AddWithValue("@vehicleId", fuelRecorderModel.VehicleId);

                var newId = Convert.ToInt32(await command.ExecuteScalarAsync());

                Console.WriteLine($"✅ Ny FuelRecord lagt til med ID: {newId}");
                return Results.Ok(new { Id = newId, Message = "Fuel record added successfully!" });
            }
        }
    }

    public async Task<IResult> UpdateFuelRecord(FuelRecorderModel fuelRecorderModel)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            
            var query = "UPDATE FuelRecords SET Kilometer = @kilometer,  FuelFilled = @fuelFilled, Price = @price, UserId = @userId, VehicleId = @vehicleId WHERE Id = @id";
            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@kilometer", fuelRecorderModel.Kilometer);
                command.Parameters.AddWithValue("@fuelFilled", fuelRecorderModel.FuelFilled);
                command.Parameters.AddWithValue("@price", fuelRecorderModel.Price);
                command.Parameters.AddWithValue("@userId", fuelRecorderModel.UserId);
                command.Parameters.AddWithValue("@vehicleId", fuelRecorderModel.VehicleId);
                
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return Results.Ok();
                }
                else
                {
                    return Results.BadRequest("Error ved opplasting av data");
                }
            }
        }
    }
    public async Task<IResult> DeleteFuelRecord(int id)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            
            var query = "DELETE FROM FuelRecords WHERE Id = @id";
            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    return Results.Ok();
                }
                else
                {
                    return Results.BadRequest("Error ved opplasting av data");
                }
            }
        }
    }

    private async Task<List<FuelRecorderModel>> SetValuesFromQuery(SqlCommand sqlcommand)
    {
        var fuelRecorders = new List<FuelRecorderModel>();

        var reader = await sqlcommand.ExecuteReaderAsync();
        
        while (await reader.ReadAsync())
        {
            fuelRecorders.Add( new FuelRecorderModel
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Kilometer = Convert.ToDouble(reader["Kilometer"]),
                FuelFilled = Convert.ToDouble(reader["FuelFilled"]),
                Price = Convert.ToDouble(reader["Price"]),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                VehicleId = reader.GetInt32(reader.GetOrdinal("VehicleId")),
            });
        }

        return fuelRecorders;
    }
}