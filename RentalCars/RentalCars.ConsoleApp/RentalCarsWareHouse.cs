using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RentalCars.API.Model;

namespace RentalCars.ConsoleApp;
using RentalCars.API.Controllers;

public class RentalCarsWareHouse
{
    public List<Car> Cars { get; set; } = new List<Car>();

    public RentalCarsWareHouse()
    {
        Cars = CreateListOfCars().Result;
    }

    public async Task<List<Car>> CreateListOfCars()
    {
        var cars = new List<Car>();
        var hei = new RentalCars.API.Controllers.CarsController();
        var da = await hei.GetAllCars();
        foreach (var car in da)
        {
            cars.Add(new Car(car.Id, car.Brand, car.ModelYear, car.Seats, car.WheelDrive, car.Fuel));
        }
        return cars;
    }


    public async void Run()
    {
        foreach (var car in Cars)
        {
            Console.WriteLine($"{car.Id} | {car.Brand} | {car.ModelYear} | {car.Seats} | {car.WheelDrive} | {car.Fuel}");
        }
    }
}