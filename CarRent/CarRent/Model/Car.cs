namespace CarRent.Model;

public class Car
{

    public int Id { get; set; }
    public string Brand { get; set; }
    public string ModelYear { get; set; }
    public int Seats { get; set; }
    public string WheelDrive { get; set; }
    public string Fuel { get; set; }
    
    public Car(int id, string brand, string modelYear, int seats, string wheelDrive, string fuel)
    {
        Id = id;
        Brand = brand;
        ModelYear = modelYear;
        Seats = seats;
        WheelDrive = wheelDrive;
        Fuel = fuel;
    }
}
