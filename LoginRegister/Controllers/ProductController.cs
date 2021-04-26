using Entity.Business.Abstract;
using Entity.Domain.Entity;
using Entity.Utilities.Constant;
using Oblivion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Oblivion.Controllers
{
    
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index(int category)
        {
            var model = new ProductModel
            {
                Products = category > 0 ? _productService.GetListByCategory(category).Data : _productService.GetList().Data

            };
            return View(model);
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public IActionResult GetProducts(int category)
        {
            //ViewBag.Message = "Hello"; //TempData["Message"];
            //ViewData["Message"] = "Welcome Oblivion";


            //var customers = new List<Product>();
            //var product = new List<ProductModel>();

            //foreach (var item in customers)
            //{
            //    var model = new ProductModel
            //    {
            //      ProductId=item.ProductId,
            //      ProductName=item.ProductName

            //    };

            //    product.Add(model);
            //}
           
            var model = new ProductModel
            {
                Products = category > 0 ? _productService.GetListByCategory(category).Data : _productService.GetList().Data

            };
            
            return View(model);
            

            //return View(product);
        }

        //[HttpGet]
        //public IActionResult GetProducts(int id)
        //{
        //    var customer = _productService.GetById(id);

        //    return View();
        //}


        [HttpGet]
        public IActionResult Add()
        {
            return View(new ProductModel());
        }
        [HttpPost]
        public IActionResult Add(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var user = new Product
                {
                     
                    ProductName = product.ProductName,
                    CategoryId = product.CategoryId,
                    Description = product.Description,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock
                };
                _productService.Add(user);
                TempData["message"] = Messages.ProductAdded;
                return RedirectToAction("GetProducts");
            }
            else
            {
                return View(product);
            }
        }


     
        [HttpGet]
        public IActionResult Delete(int productId)
        {
            var products = _productService.GetById(productId);
            _productService.Delete(products.Data);
            TempData["message"] = Messages.ProductDeleted;
            return RedirectToAction("GetProducts");
        }
        [HttpGet]
        public IActionResult Update(int productId)
        {
            ViewData["message"] = Messages.UpdateView;
            var products = _productService.GetById(productId).Data;

            var user = new ProductModel
            {
                
                ProductName = products.ProductName,
                CategoryId = products.CategoryId,
                Description = products.Description,
                UnitPrice = products.UnitPrice,
                UnitsInStock = products.UnitsInStock

            };
            //var users = _productService.Update(user);
            return View("Update", user);
        }
        [HttpPost]
        public IActionResult Update(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetById(productModel.ProductId).Data;

                product.ProductName = productModel.ProductName;
                product.CategoryId = productModel.CategoryId;
                product.Description = productModel.Description;
                product.UnitPrice = productModel.UnitPrice;
                product.UnitsInStock = productModel.UnitsInStock;
                _productService.Update(product);
                TempData["Message"] = Messages.ProductUpdated;
                return RedirectToAction("GetProducts");

            }
            else
            {
                return View("Update", productModel);
            }     
        }
         
        public IActionResult Search(string productName)
        {
            Product product = new Product();

            return View();
        }
    }
}
