using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.FoodTypes
{
    public class CreateModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]

        public FoodType FoodType { get; set; }

        public CreateModel(RestaurantDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {           
            if (ModelState.IsValid)
            {
                await _db.RestaurantFoodType.AddAsync(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food Type created sucessfully";
                return RedirectToPage("Index");
            }
            return Page();            
        }
    }
}
