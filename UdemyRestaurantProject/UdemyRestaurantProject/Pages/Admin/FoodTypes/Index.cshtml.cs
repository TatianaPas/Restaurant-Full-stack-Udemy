using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        public IEnumerable<FoodType> FoodTypes { get; set; }

        public IndexModel(RestaurantDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            FoodTypes = _db.RestaurantFoodType;
        }
    }
}
