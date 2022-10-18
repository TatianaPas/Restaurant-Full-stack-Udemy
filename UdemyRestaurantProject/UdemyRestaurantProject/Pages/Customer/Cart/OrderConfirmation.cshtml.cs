using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utilities;
using Stripe.Checkout;

namespace UdemyRestaurantProject.Pages.Customer.Cart
{
    [Authorize]
    [BindProperties]
    public class OrderConfirmationModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        public int OrderId { get; set; }
        public OrderConfirmationModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void OnGet(int id)
        {
           OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(x => x.Id == id);
            if(orderHeader.SessionId!=null)
            {
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);
                if(session.PaymentStatus.ToLower()=="paid")
                {
                    orderHeader.Status = SD.StatusSubmitted;
                    _unitOfWork.Save();
                }
            }
            List<ShoppingCart> shoppingCarts = 
                _unitOfWork.ShoppingCart.GetAll(u =>u.ApplicationUserId==orderHeader.UserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            OrderId = id;
        }
    }
}
