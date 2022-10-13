using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodType FoodT { get; set; }
        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            FoodT = _unitOfWork.FoodType.GetFirstOrDefault(u=>u.Id==id);
        }

        public async Task<IActionResult> OnPost()
        {           
            if (ModelState.IsValid)
            {
                _unitOfWork.FoodType.Update(FoodT);
                _unitOfWork.Save();
                TempData["success"] = "Food Type updated sucessfully";
                return RedirectToPage("Index");
            }
            return Page();            
        }
    }
}
