using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyRestaurantProject.Data;
using UdemyRestaurantProject.Model;

namespace UdemyRestaurantProject.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]

        public Category Category { get; set; }

        public CreateModel(RestaurantDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            await _db.RestaurantCategory.AddAsync(Category);
            await _db.SaveChangesAsync();
            return RedirectToPage("Index");

        }
    }
}
