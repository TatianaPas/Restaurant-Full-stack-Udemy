using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]

        public Category Category { get; set; }

        public EditModel(RestaurantDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.RestaurantCategory.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order can not exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _db.RestaurantCategory.Update(Category);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category updated sucessfully";
                return RedirectToPage("Index");
            }
            return Page();            
        }
    }
}
