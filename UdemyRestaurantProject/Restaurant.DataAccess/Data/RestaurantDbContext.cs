using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.DataAccess.Data
{
    public class RestaurantDbContext :DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options):base(options)
        {

        }
        public DbSet<Category> RestaurantCategory { get; set; }
        public DbSet<FoodType> RestaurantFoodType { get; set; }
        public DbSet<MenuItem> RestaurantMenuItem { get; set; }
    }
}
