using Microsoft.EntityFrameworkCore;
using RestaurantService.Models;

namespace RestaurantService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {

    }

    public DbSet<Restaurant> Restaurants { get; set; }
}