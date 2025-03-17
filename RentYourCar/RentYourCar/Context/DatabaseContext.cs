using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using RentYourCar.Models;

namespace RentYourCar.Context;


public class DatabaseContext : DbContext 
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Car> Cars { get; set; }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Employees>().ToTable("employees");
    //     modelBuilder.Entity<CarWareHouse>().ToTable("car_warehouse");
    // }
}