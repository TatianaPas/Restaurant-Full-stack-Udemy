using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.Categories
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
