using Fuelrecorder.Database;
using Fuelrecorder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fuelrecorder.Controllers;

[ApiController]
[Route("[controller]")]
public class FuelRecorderController : ControllerBase
{
    private readonly FuelRecords _db;
    
    public FuelRecorderController(FuelRecords db)
    {
        _db = db;
    }
    // GET
    [HttpGet, Authorize]
    public async Task<IResult> GetAllRecords() => await _db.GetAll();
    
    [HttpGet("{id}"), Authorize]
    public async Task<IResult> GetAllRecordsFromUser(int id) => 
        await _db.GetAllFuelRecordsFromUserByUserId(id);
    
    [HttpGet("/FuelRecorder/V/{id}"), Authorize]
    public async Task<IResult> GetAllFuelRecordsFromUserByVehicleId(int id) => 
        await _db.GetAllFuelRecordsFromUserByVehicleId(id);
    
    [HttpPut("{id}"), Authorize]
    public async Task<IResult> UpdateRecord([FromBody] FuelRecorderModel updatedRecord) =>
        await _db.UpdateFuelRecord(updatedRecord);
    

    [HttpPost, Authorize]
    public async Task<IResult> AddNewRecord([FromBody] FuelRecorderModel newRecord) =>
        await _db.AddFuelRecord(newRecord);

    [HttpDelete("{id}"), Authorize]
    public async Task<IResult> DeleteRecord(int id) => 
        await _db.DeleteFuelRecord(id);
}