using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using YourRentalCarAPI.Data;
using YourRentalCarAPI.Entities;

namespace YourRentalCarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarInnStockController : ControllerBase
    {
        private readonly DataContext _context;

        public CarInnStockController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarInnStock>>> GetAllCars()
        {
            var cars = await _context.CarsInnStock.ToListAsync();

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarInnStock>> GetCarById(int id)
        {
            var car = await _context.CarsInnStock.FindAsync(id);
            if (car is null)
            {
                return NotFound("Car not found.");
            }

            return Ok(car);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<CarInnStock>> AddCar(CarInnStock carInnStock)
        {
            _context.CarsInnStock.Add(carInnStock);
            await _context.SaveChangesAsync();

            return Ok(carInnStock);
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<List<CarInnStock>>> UpdateEmployee(CarInnStock updateCarInnStock)
        {
            var dbcarInnStock = await _context.CarsInnStock.FindAsync(updateCarInnStock.Id);
            if (dbcarInnStock is null)
            {
                return NotFound("Employee not found.");
            }
            else
            {
                dbcarInnStock.Brand = updateCarInnStock.Brand;
                dbcarInnStock.ModelYear = updateCarInnStock.ModelYear;
                dbcarInnStock.WheelDrive = updateCarInnStock.WheelDrive;
                dbcarInnStock.Seats = updateCarInnStock.Seats;
                dbcarInnStock.Fuel = updateCarInnStock.Fuel;
                dbcarInnStock.Picture = updateCarInnStock.Picture;

                await _context.SaveChangesAsync();
            }

            return Ok(await _context.CarsInnStock.ToListAsync());
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<List<CarInnStock>>> DeleteCar(int id)
        {
            var dbcarInnStock = await _context.CarsInnStock.FindAsync(id);
            if (dbcarInnStock is null)
            {
                return NotFound("Car not found.");
            }
            else
            {
                _context.CarsInnStock.Remove(dbcarInnStock);
                await _context.SaveChangesAsync();
            }

            return Ok(await _context.CarsInnStock.ToListAsync());
        }
    }

}
