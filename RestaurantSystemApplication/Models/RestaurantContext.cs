using Microsoft.EntityFrameworkCore;

namespace RestaurantSystemApplication.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Plate> Plates { get; set; }
        public DbSet<Serve> Serves { get; set; }
    }
}
