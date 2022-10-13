using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Data;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;

namespace UdemyRestaurantProject.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodType FoodT { get; set; }
        public CreateModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {           
            if (ModelState.IsValid)
            {
                _unitOfWork.FoodType.Add(FoodT);
                _unitOfWork.Save();
                TempData["success"] = "Food Type created sucessfully";
                return RedirectToPage("Index");
            }
            return Page();            
        }
    }
}
