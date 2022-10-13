using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.FoodTypes
{
    public class DeleteModel : PageModel
    {
        private readonly RestaurantDbContext _db;
        [BindProperty]

        public FoodType FoodType { get; set; }

        public DeleteModel(RestaurantDbContext db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            FoodType = _db.RestaurantFoodType.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {


            var foodTypeFromDb = _db.RestaurantFoodType.Find(FoodType.Id);
            if (foodTypeFromDb != null)
            {
                _db.RestaurantFoodType.Remove(foodTypeFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food Type deleted sucessfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
