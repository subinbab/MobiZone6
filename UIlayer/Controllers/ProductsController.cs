using DomainLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UIlayer.Data.ApiServices;

namespace UIlayer.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            IEnumerable<Product> products = ProductApi.GetProduct();
            return View(products);
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Product product = ProductApi.GetProduct(id);
            List<Product> products = new List<Product>();
            products.Add(product);
            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool result = ProductApi.EditProduct(product);
            if (result)
            {
                return RedirectToAction("/");
            }
            return RedirectToAction("/");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product employee = ProductApi.GetProduct(id);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(Product employee)
        {
            bool result = ProductApi.EditProduct(employee);
            if (result)
            {
                return RedirectToAction("/");
            }
            return RedirectToAction("/");
        }
        public ActionResult Delete(int id)
        {
            bool result = ProductApi.DeleteProduct(id);
            if (result)
            {
                return RedirectToAction("/");
            }
            return RedirectToAction("unsucces");
        }
        public PartialViewResult _Create()
        {
            return PartialView();
        }
    }
}
