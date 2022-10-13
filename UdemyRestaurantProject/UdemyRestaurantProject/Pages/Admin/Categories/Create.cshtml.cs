using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.Categories
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
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order can not exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                await _db.RestaurantCategory.AddAsync(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category created sucessfully";
                return RedirectToPage("Index");
            }
            return Page();            
        }
    }
}
