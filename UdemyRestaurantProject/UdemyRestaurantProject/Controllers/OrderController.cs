using Microsoft.AspNetCore.Mvc;
using Restaurant.DataAccess.Repository.IRepository;
using Restaurant.Utilities;

namespace UdemyRestaurantProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        

        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Authorized]

        public IActionResult Get(string? status=null)
        {
            var orderHeadersLst = _unitOfWork.OrderHeader.GetAll(includeProperties:"ApplicationUser");
            if(status== "cancelled")
            {
                orderHeadersLst = orderHeadersLst.Where(x => x.Status == SD.StatusCancelled || x.Status == SD.StatusRejected);
            }
            else
            {
                if (status == "completed")
                {
                    orderHeadersLst = orderHeadersLst.Where(x => x.Status == SD.StatusCompleted );
                }
                else
                {
                    if (status == "ready")
                    {
                        orderHeadersLst = orderHeadersLst.Where(x => x.Status == SD.StatusReady);
                    }
                    else
                    {
                            orderHeadersLst = orderHeadersLst.Where(x => x.Status == SD.StatusInProcess || x.Status == SD.StatusSubmitted);
                    }

                }

            }
           
              return Json(new { data = orderHeadersLst });           
        }
    }
}
