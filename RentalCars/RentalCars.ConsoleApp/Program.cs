using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using RentalCars.ConsoleApp;
using Dapper;
var wareHouse = new RentalCarsWareHouse();

wareHouse.Run();
// var cars = new List<Car>();
// var hei = new RentalCars.API.Controllers.CarsController();
// var da = await hei.GetAllCars();
// foreach (var car in da)
// {
//     cars.Add(new Car(car.Id, car.Brand, car.ModelYear, car.Seats, car.WheelDrive, car.Fuel));
// }
// var baseUrl = "https://localhost:7146/RentalCarsCars";
// var client = new HttpClient();
// var response = await client.GetAsync($"{baseUrl}");
// var data = await response.Content.ReadAsStringAsync();

//
// Console.WriteLine(cars[0].Id);
Console.WriteLine("hei");