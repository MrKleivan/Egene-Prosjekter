namespace RentalCars.ConsoleApp;

public class InStock
{

    public int Id { get; set; }
    public string Brand { get; set; }
    public int ModelYear { get; set; }
    public int Seats { get; set; }
    public string WheelDrive { get; set; }
    public string Fuel { get; set; }
    public int Price { get; set; }
    public string Available { get; set; }
    
    public InStock(int id, string brand, int modelYear, int seats, string wheelDrive, string fuel, int price, string available)
    {
        Id = id;
        Brand = brand;
        ModelYear = modelYear;
        Seats = seats;
        WheelDrive = wheelDrive;
        Fuel = fuel;
        Price = price;
        Available = available;
    }
    
}