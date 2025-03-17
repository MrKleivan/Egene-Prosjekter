namespace RentYourCar.Models;

public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int ModelYear { get; set; }
    public string WheelDrive { get; set; }
    public string FuelType { get; set; }
    public int Seats { get; set; }
    public int DayPrice { get; set; }
    public string Available { get; set; }
}