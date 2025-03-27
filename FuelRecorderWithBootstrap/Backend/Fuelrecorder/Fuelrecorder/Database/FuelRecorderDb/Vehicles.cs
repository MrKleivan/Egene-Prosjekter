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
        var conn = new SqlConnection(_connectionString);
        
        await conn.OpenAsync();
        var query = "SELECT * FROM Vehicles";
        var sql = new SqlCommand(query, conn);

        vehicles = await SetValuesFromQuery(sql);

        if (vehicles.Count > 0)
        {
            return Results.BadRequest("Ingen kjøretøy funnet!");
        }
        
        return Results.Ok(vehicles);
    }

    public async Task<IResult> GetAllFromUserByUserId(int id)
    {
        var vehicles = new List<VehiclesModel>();
        var conn = new SqlConnection(_connectionString);
        
        await conn.OpenAsync();
        var sql = new SqlCommand("SELECT * FROM Vehicles WHERE UserId = @id", conn);
        sql.Parameters.AddWithValue("@id", id);

        vehicles = await SetValuesFromQuery(sql);
        
        conn.Close();
        
        return Results.Ok(vehicles);
    }
    
    public async Task<IResult> AddVehicle(VehiclesModel vehicle)
    {
        var vehicles = new List<VehiclesModel>();
        var conn = new SqlConnection(_connectionString);
        
        await conn.OpenAsync();
        
        var query = "INSERT INTO Vehicles (Type, Name, Brand, Fuel, StartKilometer, UserId) VALUES (@type, @name, @brand, @fuel, @startKilometer, @userId)";
        var sql = new SqlCommand(query, conn);
            
        sql.Parameters.AddWithValue("@type", vehicle.Type);
        sql.Parameters.AddWithValue("@name", vehicle.Name);
        sql.Parameters.AddWithValue("@brand", vehicle.Brand);
        sql.Parameters.AddWithValue("@fuel", vehicle.Fuel);
        sql.Parameters.AddWithValue("@startKilometer", vehicle.StartKilometer);
        sql.Parameters.AddWithValue("@userId", vehicle.UserId);

        vehicles = await SetValuesFromQuery(sql);
        
        conn.Close();
        
        if (vehicles.Count > 0)
        {
            return Results.BadRequest("Ingen kjøretøy ble registrert");
        }
        
        return Results.Ok(vehicles);
    }
    
    public async Task<IResult> UpdateVehicle(VehiclesModel vehicle)
    {
        var vehicles = new List<VehiclesModel>();
        var conn = new SqlConnection(_connectionString);
        
        await conn.OpenAsync();
        
        var query = "UPDATE Vehicles SET Type = @type, Name = @name, Brand = @brand, Fuel = @fuel, StartKilometer = @startKilometer, UserId = @userId WHERE Id = @id";
        var sql = new SqlCommand(query, conn);
            
        sql.Parameters.AddWithValue("@type", vehicle.Type);
        sql.Parameters.AddWithValue("@name", vehicle.Name);
        sql.Parameters.AddWithValue("@brand", vehicle.Brand);
        sql.Parameters.AddWithValue("@fuel", vehicle.Fuel);
        sql.Parameters.AddWithValue("@startKilometer", vehicle.StartKilometer);
        sql.Parameters.AddWithValue("@userId", vehicle.UserId);
        sql.Parameters.AddWithValue("@id", vehicle.Id);
               
        vehicles = await SetValuesFromQuery(sql);
        
        conn.Close();
        
        if (vehicles.Count > 0)
        {
            return Results.BadRequest("Oppdatering feilet");
        }
        return Results.Ok(vehicle);
    }
    
    public async Task<IResult> DeleteVehicle(int id)
    {
        var vehicles = new List<VehiclesModel>();
        var conn = new SqlConnection(_connectionString);
        
        await conn.OpenAsync();
        
        var deleteFuelRecordsQuery = "DELETE FROM FuelRecords WHERE VehicleId = @id";
        using (var command = new SqlCommand(deleteFuelRecordsQuery, conn))
        {
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();
        }
            
        var query = "DELETE FROM Vehicles WHERE Id = @id";
        var sql = new SqlCommand(query, conn);
        
        sql.Parameters.AddWithValue("@id", id);
        
        vehicles = await SetValuesFromQuery(sql);
        
        conn.Close();
             
        if (vehicles.Count > 0)
        {
            return Results.BadRequest("Sletting feilet");
        }
        
        return Results.Ok(vehicles);
    }
    
    private async Task<List<VehiclesModel>> SetValuesFromQuery(SqlCommand sqlcommand)
    {
        var vehicles = new List<VehiclesModel>();

        var reader = await sqlcommand.ExecuteReaderAsync();
        
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

        return vehicles;
    }
}