using System.Data.SqlClient;
using Fuelrecorder.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Fuelrecorder.Database;

public class Vehicles
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public Vehicles(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<IResult> GetAll()
    {
        
        var vehicles = new List<VehiclesModel>();
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = new SqlCommand("SELECT * FROM Vehicles", conn);

            using (var reader = await sql.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    vehicles.Add( new VehiclesModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Type = reader.GetString(reader.GetOrdinal("Type")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Brand = reader.GetString(reader.GetOrdinal("Brand")),
                        Fuel = reader.GetString(reader.GetOrdinal("Fuel")),
                        StartKilometer = reader.GetInt32(reader.GetOrdinal("StartKilometer")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    });
                }
                
            }
        }
        return Results.Ok(vehicles);
    }

    public async Task<IResult> GetAllFromUserByUserId(int id)
    {
        var vehicles = new List<VehiclesModel>();
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = new SqlCommand("SELECT * FROM Vehicles WHERE UserId = @id", conn);
            sql.Parameters.AddWithValue("@id", id);

            using (var reader = await sql.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    vehicles.Add(new VehiclesModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Type = reader.GetString(reader.GetOrdinal("Type")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Brand = reader.GetString(reader.GetOrdinal("Brand")),
                        Fuel = reader.GetString(reader.GetOrdinal("Fuel")),
                        StartKilometer = reader.GetInt32(reader.GetOrdinal("Id")),
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                    });
                }

            }
        }
        return Results.Ok(vehicles);
    }
    
    public async Task<IResult> AddVehicle(VehiclesModel vehicle)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            
            var query = "INSERT INTO Vehicles (Type, Name, Brand, Fuel, StartKilometer, UserId) VALUES (@type, @name, @brand, @fuel, @startKilometer, @userId)";
            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@type", vehicle.Type);
                command.Parameters.AddWithValue("@name", vehicle.Name);
                command.Parameters.AddWithValue("@brand", vehicle.Brand);
                command.Parameters.AddWithValue("@fuel", vehicle.Fuel);
                command.Parameters.AddWithValue("@startKilometer", vehicle.StartKilometer);
                command.Parameters.AddWithValue("@userId", vehicle.UserId);
                
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
    
    public async Task<IResult> UpdateVehicle(VehiclesModel vehicle)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            
            var query = "UPDATE Vehicles SET Type = @type, Name = @name, Brand = @brand, Fuel = @fuel, StartKilometer = @startKilometer, UserId = @userId WHERE Id = @id";
            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@type", vehicle.Type);
                command.Parameters.AddWithValue("@name", vehicle.Name);
                command.Parameters.AddWithValue("@brand", vehicle.Brand);
                command.Parameters.AddWithValue("@fuel", vehicle.Fuel);
                command.Parameters.AddWithValue("@startKilometer", vehicle.StartKilometer);
                command.Parameters.AddWithValue("@userId", vehicle.UserId);
                command.Parameters.AddWithValue("@id", vehicle.Id);
                
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
    
    public async Task<IResult> DeleteVehicle(int id)
    {
        using (var conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            
            var deleteFuelRecordsQuery = "DELETE FROM FuelRecords WHERE VehicleId = @id";
            using (var command = new SqlCommand(deleteFuelRecordsQuery, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                await command.ExecuteNonQueryAsync();
            }
            
            var query = "DELETE FROM Vehicles WHERE Id = @id";
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