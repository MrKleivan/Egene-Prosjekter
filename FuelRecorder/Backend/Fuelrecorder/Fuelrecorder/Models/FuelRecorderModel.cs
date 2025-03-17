namespace Fuelrecorder.Models;

public class FuelRecorderModel
{
    public int Id { get; set; }
    public double Kilometer { get; set; }
    public double FuelFilled { get; set; }
    public double Price { get; set; }
    public int UserId { get; set; }
    public int VehicleId { get; set; }
}