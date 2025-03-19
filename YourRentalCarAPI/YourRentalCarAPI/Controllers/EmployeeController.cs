using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YourRentalCarAPI.Data;
using YourRentalCarAPI.Entities;

namespace YourRentalCarAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee is null)
            {
                return NotFound("Employee not found.");
            }

            return Ok(employee);
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<List<Employee>>> AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpPut, Authorize]
        public async Task<ActionResult<List<Employee>>> UpdateEmployee(Employee updateEmployee)
        {
            var dbemployee = await _context.Employees.FindAsync(updateEmployee.Id);
            if (dbemployee is null)
            {
                return NotFound("Employee not found.");
            }
            else
            {
                dbemployee.FirstName = updateEmployee.FirstName;
                dbemployee.LastName = updateEmployee.LastName;
                dbemployee.Title = updateEmployee.Title;

                await _context.SaveChangesAsync();
            }

            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpDelete("{id}"), Authorize]
        public async Task<ActionResult<List<Employee>>> DeleteEmployee(int id)
        {
            var dbemployee = await _context.Employees.FindAsync(id);
            if (dbemployee is null)
            {
                return NotFound("Employee not found.");
            }
            else
            {
                _context.Employees.Remove(dbemployee);
                await _context.SaveChangesAsync();
            }

            return Ok(await _context.Employees.ToListAsync());
        }
    }
}
