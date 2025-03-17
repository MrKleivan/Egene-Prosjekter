using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using RentalCars.API.Model;

namespace RentalCars.API.Controllers;
[ApiController]
[Route("[controller]")]

public class CarsController : ControllerBase
{
    
    
    private readonly string connectionString = "Server=KLEIVAN\\SQLEXPRESS;Database=RentalCars;Trusted_Connection=True;TrustServerCertificate=True";
    
    // GET
    [HttpGet]
    public async Task<IEnumerable<Car>> GetAllCars()
    {
        const string sql = "SELECT * FROM Cars";
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cars = conn.Query<Car>(sql);
            Ok(cars);
            return cars;
        }
        
    }

    [HttpGet ("{Id}")]
    public async Task<IActionResult> GetCarById(int Id)
    {
        string sql = $"SELECT * FROM Cars WHERE Cars.Id = {Id};";
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cars = conn.Query<Car>(sql);
            return Ok(cars);
        }
        
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCarToCars()
    {
        const string sql = "INSERT INTO Cars (Brand, ModelYear, Seats, WheelDrive, Fuel) VALUES ('BMW', '1999', '5', 'Back', 'Diesel');";
        using (var conn = new SqlConnection(connectionString))
        {
            conn.Open();
            var cars = conn.Query<Car>(sql);
            return Ok(cars);
        }
    }
    
    // [HttpPost(Name = "AddCarToInStock")]
    // public async Task<IActionResult> AddCarToInStock()
    // {
    //     const string sql = "INSERT INTO InStock (Id, CarId, Price, Available) VALUES ('4', '23', '450', 'Yes');";
    //     using (var conn = new SqlConnection(connectionString))
    //     {
    //         conn.Open();
    //         var cars = conn.Query<Car>(sql);
    //         return Ok(cars);
    //     }
    // }
    

    
    
}

