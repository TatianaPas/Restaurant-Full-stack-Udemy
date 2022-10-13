using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]

        public Category Category { get; set; }

        public DeleteModel(RestaurantDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Category = _db.RestaurantCategory.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {           


                var categoryFromDb = _db.RestaurantCategory.Find(Category.Id);  
                if(categoryFromDb != null)
                {
                    _db.RestaurantCategory.Remove(categoryFromDb);
                    await _db.SaveChangesAsync();
                TempData["success"] = "Category deleted sucessfully";
                return RedirectToPage("Index");
                }                        
            return Page();            
        }
    }
}
