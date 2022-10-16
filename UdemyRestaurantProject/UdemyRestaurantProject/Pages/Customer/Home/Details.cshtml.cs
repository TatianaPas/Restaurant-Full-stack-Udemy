using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using System.ComponentModel.DataAnnotations;

namespace UdemyRestaurantProject.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public MenuItem MenuItem { get; set; }
        [Range(1,20,ErrorMessage="Please select up to 20 items")]
        public int Count { get; set; }

        public void OnGet(int id)
        {
            MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.Id == id, includeProperties:"Category,FoodType");
        }
    }
}
