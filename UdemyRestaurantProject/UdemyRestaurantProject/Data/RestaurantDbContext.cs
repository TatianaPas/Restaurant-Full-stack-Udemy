using Microsoft.EntityFrameworkCore;
using UdemyRestaurantProject.Model;

namespace UdemyRestaurantProject.Data
{
    public class RestaurantDbContext :DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options):base(options)
        {

        }
        public DbSet<Category> RestaurantCategory { get; set; }
    }
}
