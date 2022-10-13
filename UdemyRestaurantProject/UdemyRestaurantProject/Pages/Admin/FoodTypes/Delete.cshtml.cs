using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodType FoodT { get; set; }
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FoodT = _unitOfWork.FoodType.GetFirstOrDefault(u=>u.Id==id);
        }

        public async Task<IActionResult> OnPost(int id)
        {


            var foodTypeFromDb = _unitOfWork.FoodType.GetFirstOrDefault(u => u.Id == id);
            if (foodTypeFromDb != null)
            {
                _unitOfWork.FoodType.Remove(foodTypeFromDb);
                _unitOfWork.Save();
                TempData["success"] = "Food Type deleted sucessfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
