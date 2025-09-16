using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Restaurant>()
                .HasMany(c => c.Items)
                .WithOne(a => a.Restaurant!)
                .HasForeignKey(a => a.RestaurantId);

            modelBuilder
                .Entity<Item>()
                .HasOne(a => a.Restaurant)
                .WithMany(c => c.Items)
                .HasForeignKey(a => a.RestaurantId);
        }
    }
}