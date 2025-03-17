using Microsoft.AspNetCore.Mvc;

namespace RentYourCar.Controllers;

public class EmployeeController : Controller
{
    // GET
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
}