using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.FoodTypes
{
    public class EditModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]

        public FoodType FoodType { get; set; }

        public EditModel(RestaurantDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            FoodType = _db.RestaurantFoodType.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {           
            if (ModelState.IsValid)
            {
                _db.RestaurantFoodType.Update(FoodType);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food Type updated sucessfully";
                return RedirectToPage("Index");
            }
            return Page();            
        }
    }
}
