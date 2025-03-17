namespace Fuelrecorder.Models;

public class VehiclesModel
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Brand { get; set; }
    public string Fuel { get; set; }
    public int StartKilometer { get; set; }
    public int UserId { get; set; }
}