using Entity.Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Oblivion.Models;

namespace Oblivion.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly IOrderService _shippingService;
        private readonly IOrderDetailService _orderDetailService;
        public OrderController(IOrderService shippingService,IOrderDetailService orderDetailService)
        {
            _shippingService = shippingService;
            _orderDetailService = orderDetailService;
        }
        public IActionResult Ship()
        {
            var model = new OrderModel
            {
                Orders = _shippingService.GetAll()
            };
            return View(model);
        }
        public IActionResult OrderDetail()
        {
            var model = new OrderDetailModel
            {
                orderDetail = _orderDetailService.GetAll()
            };
            return View(model);
        }
    }
}
