using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Models;
using Restaurant.Utilities;
using Stripe.BillingPortal;
using Stripe.Checkout;
using System.Security.Claims;

namespace UdemyRestaurantProject.Pages.Customer.Cart
{
    [Authorize]
    [BindProperties]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }

        private readonly IUnitOfWork _unitOfWork;
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
        }
        public void OnGet()

        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

                foreach (var item in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (item.MenuItem.Price * item.Count);
                }
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);
                OrderHeader.PickupName = applicationUser.FirstName;

            }


        }

        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: u => u.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.FoodType,MenuItem.Category");

                foreach (var item in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (item.MenuItem.Price * item.Count);
                }

                OrderHeader.Status = SD.StatusPending;
                OrderHeader.OrderDate = DateTime.Now;
                OrderHeader.UserId = claim.Value;
                OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString() + " " +
                OrderHeader.PickUpTime.ToShortTimeString());
                _unitOfWork.OrderHeader.Add(OrderHeader);
                _unitOfWork.Save();

                foreach (var item in ShoppingCartList)
                {
                    OrderDetails orderDetails = new()
                    {
                        MenuItemId = item.MenuItemId,
                        OrderId = OrderHeader.Id,
                        Name = item.MenuItem.Name,
                        Price = item.MenuItem.Price,
                        Count = item.Count
                    };
                    _unitOfWork.OrderDetails.Add(orderDetails);
                }

              //  _unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
                _unitOfWork.Save();

                //Stripe payment

                var domain = "https://localhost:44323/";
                var options = new Stripe.Checkout.SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>()
                    ,

                    PaymentMethodTypes = new List<string>
                    {
                        "card",
                    },
                    Mode = "payment",
                    SuccessUrl = domain + $"Customer/Cart/OrderConfirmation?id={OrderHeader.Id}",
                    CancelUrl = domain + "Customer/Cart/Index",
                };

                //add line items
                foreach(var item in ShoppingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.MenuItem.Price * 100),
                            Currency = "nzd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.MenuItem.Name
                            },
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem); 
                }               

                var service = new Stripe.Checkout.SessionService();
                Stripe.Checkout.Session session = service.Create(options);

                Response.Headers.Add("Location", session.Url);

                OrderHeader.PaymentIntetnId = session.PaymentIntentId;
                OrderHeader.SessionId = session.Id;
                _unitOfWork.Save();

                return new StatusCodeResult(303);
            }
            return Page();
        }
    }
}
