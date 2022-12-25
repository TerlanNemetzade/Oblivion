using Entity.Business.Abstract;
using Entity.Domain.Entity;
using Entity.Dto;
using Entity.Utilities;
using Entity.Utilities.Constant;
using Oblivion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oblivion.Helpers;
using System.Linq;
using System.Security.Claims;
using Entity.DomainModel;

namespace Oblivion.Controllers
{
  
    public class CartController : Controller
    {
        private ICartService _cartService;
        private ICartSessionHelper _cartSessionHelper;
        private IProductService _productService;
        private IOrderService _orderService;
         
        public CartController(ICartService cartService,ICartSessionHelper cartSessionHelper,
            IProductService productService,IOrderService orderService)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _productService = productService;
            _orderService = orderService;
        }
        [Authorize]
        public IActionResult AddToCart(int productId)
        {
            
            //if (user.UserName == null) 
            //{
            //    RedirectToAction("Login", "Account");
            //}
            IDataResult<Product> product = _productService.GetById(productId);
            var cart = _cartSessionHelper.GetCart("cart");
            _cartService.AddToCart(cart, product.Data);
            _cartSessionHelper.SetCart("cart", cart);
            TempData.Add("message", product.Data.ProductName + " added to cart!");
            return RedirectToAction("Index", "Product");
        }
        public IActionResult Index()
        {
            var model = new CartListViewModel
            {
                Cart = _cartSessionHelper.GetCart("cart")

            };
            if (model.Cart.CartLines.Count == 0)
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View(model);
            }
           
        }
        public IActionResult RemoveFromCart(int productId)
        {
            IDataResult<Product> product = _productService.GetById(productId);
            var cart = _cartSessionHelper.GetCart("cart");
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionHelper.SetCart("cart", cart);
            TempData.Add("message", product.Data.ProductName + Messages.ProductDeleted);
            return RedirectToAction("Index", "Cart");
        }
        //[HttpGet]
        //public IActionResult Complete()
        //{
        //    //var product = _productService.GetById(productId).Data;
           
        //    return View(new OrderModel());
        //    //var model = new ShippingDetailViewModel
        //    //{
        //    //    shippings = new Shipping()
        //    //};
        //    //return View(model);
        //}
        public IActionResult Complete()
        {
            return RedirectToAction(nameof(SuccessComplete));
        }
        //public IActionResult Complete(OrderModel orderModel)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View();
        //    //}

        //    var modelCart = new CartListViewModel
        //    {
        //        Cart = _cartSessionHelper.GetCart("cart")
        //    };
        //    //var cart = new Cart();
          
        //    var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    //var model = new Order();
        //    //var product = modelCart.Cart.CartLines.FirstOrDefault(p=>productId==p.Product.ProductId);
        //    foreach(var item in modelCart.Cart.CartLines)
        //    {
        //        for(int i = 1; i <= item.Quantity; i++)
        //        {
        //            var model = new Order
        //            {
        //                UserID = GetCurrentUser(),
        //                FirstName = orderModel.FirstName,
        //                LastName = orderModel.LastName,
        //                ProductID = item.Product.ProductId
        //            };
        //            _orderService.Add(model);
        //        }
        //        //model.UserID = GetCurrentUser();
        //        //model.FirstName = orderModel.FirstName;
        //        //model.LastName = orderModel.LastName;
        //        //model.ProductID = item.Product.ProductId;
             
              
        //    }
           
       
        //    TempData.Add("message", Messages.OrderComplete);
        //    _cartSessionHelper.Clear();
        //    return RedirectToAction(nameof(SuccessComplete));
        //    //_orderService.Add(orders);
        //    //TempData.Add("message", Messages.OrderComplete);
        //    //_cartSessionHelper.Clear();
        //}
        public IActionResult SuccessComplete()
        {
            return View();
        }
        public string GetCurrentUser()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return user;
        }
    }
}
