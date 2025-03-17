using Fuelrecorder.Database;
using Fuelrecorder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuelrecorder.Controllers;

[ApiController]
[Route("[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly Vehicles _db;

    public VehiclesController(Vehicles db)
    {
        _db = db;
    }

    // GET
    [HttpGet, Authorize]
    public async Task<IResult> GetAllVehicles() => await _db.GetAll();
    
    [HttpGet("{id}"), Authorize]
    public async Task<IResult> GetAllVehiclesFromUser(int id) => 
        await _db.GetAllFromUserByUserId(id);

    [HttpPost, Authorize]
    public async Task<IResult> AddVehicles([FromBody] VehiclesModel vehicles) =>
        await _db.AddVehicle(vehicles);
   
    [HttpPut, Authorize]
    public async Task<IResult> UpdateVehicles([FromBody] VehiclesModel vehicles) =>
        await _db.UpdateVehicle(vehicles); 
    
    [HttpDelete("{id}"), Authorize]
    public async Task<IResult> DeleteVehicle(int id) => 
        await _db.DeleteVehicle(id);
}