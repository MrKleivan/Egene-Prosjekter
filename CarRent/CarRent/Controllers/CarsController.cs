using CarRent.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
 

namespace CarRent.Controllers;
[ApiController]
[Route("[controller]")]
public class CarsController : ControllerBase
{
    private readonly string _connectionString = "Server=KLEIVAN\\SQLEXPRESS;Database=RentalCars;Trusted_Connection=True;TrustServerCertificate=True;";
    private readonly ILogger<CarsController> _logger;

    public CarsController(ILogger<CarsController> logger)
    {
        _logger = logger;
    }

    // GET
    [HttpGet]
    public async Task<List<Car>> GetAll()
    {
        var cars = new List<Car>();
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var sql = new SqlCommand("SELECT * FROM Cars", connection);
            var reader = await sql.ExecuteReaderAsync();
            
            while (reader.Read())
            {
                var id = reader.GetInt32(reader.GetOrdinal("Id"));
                var brand = reader.GetString(reader.GetOrdinal("Brand"));
                var modelYear = reader.GetString(reader.GetOrdinal("ModelYear"));
                var seats = reader.GetInt32(reader.GetOrdinal("Seats"));
                var wheelDrive = reader.GetString(reader.GetOrdinal("WheelDrive"));
                var fuel = reader.GetString(reader.GetOrdinal("Fuel"));
                
                cars.Add(new Car(id, brand, modelYear, seats, wheelDrive, fuel));
            }
            
        }
        
        return cars;
    }

    
}