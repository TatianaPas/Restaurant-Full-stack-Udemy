using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Utilities;
using System.Security.Claims;

namespace UdemyRestaurantProject.ViewComponents
{
    public class ShoppingCartViewComponent:ViewComponent 
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            int count = 0;
            if(claim != null)
            {// is logged in
                if(HttpContext.Session.GetInt32(SD.SessisonCart)!=null)
                {
                    return View(HttpContext.Session.GetInt32(SD.SessisonCart));
                }
                else
                {
                    count = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                    HttpContext.Session.SetInt32(SD.SessisonCart, count);
                    return View(count);
                }
            }
            else
            {
                HttpContext.Session.Clear();
                // not logged in
                return View(count);
            }

        }
    }
}
