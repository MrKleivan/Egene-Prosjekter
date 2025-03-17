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
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = new SqlCommand("SELECT * FROM FuelRecords", conn);

            using (var reader = await sql.ExecuteReaderAsync())
            {
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
                
            }
        }

        if (fuelRecorders.Count < 1)
        {
            return Results.NotFound();
        }
        return Results.Ok(fuelRecorders);
    }
    
    public async Task<IResult> GetAllFuelRecordsFromUserByUserId(int id)
    {
        Console.Write(id);
        var fuelRecorders = new List<FuelRecorderModel>();
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = new SqlCommand("SELECT * FROM FuelRecords WHERE UserId = @id", conn);
            sql.Parameters.AddWithValue("@id", id);
            
            using (var reader = await sql.ExecuteReaderAsync())
            {
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
            }
        }

        if (fuelRecorders.Count == 0)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(fuelRecorders);
    }
    
    public async Task<IResult> GetAllFuelRecordsFromUserByVehicleId(int id)
    {
        Console.Write(id);
        var fuelRecorders = new List<FuelRecorderModel>();
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = new SqlCommand("SELECT * FROM FuelRecords WHERE VehicleId = @id", conn);
            sql.Parameters.AddWithValue("@id", id);
            
            using (var reader = await sql.ExecuteReaderAsync())
            {
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
            }
        }

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
            
            var query = "INSERT INTO FuelRecords (Kilometer, FuelFilled, Price, UserId, VehicleId) VALUES (@kilometer, @fuelFilled, @price, @userId, @vehicleId)";
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
}