using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyRestaurantProject.Data;
using UdemyRestaurantProject.Model;

namespace UdemyRestaurantProject.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        public IEnumerable<Category> Categories { get; set; }

        public IndexModel(RestaurantDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Categories = _db.RestaurantCategory;
        }
    }
}
