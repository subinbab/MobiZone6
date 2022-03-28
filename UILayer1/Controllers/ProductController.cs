using DomainLayer;
using DomainLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using UIlayer.Data.ApiServices;


namespace UIlayer.Controllers
{

    public class ProductController : Controller
    {
        Product data = null;
        IConfiguration Configuration { get; }
        ProductApi pr;
        public ProductController(IConfiguration configuration)
        {
            Configuration = configuration;
            pr = new ProductApi(Configuration);
            data = new Product();
        }
        public ActionResult Index(int? i)
        {
            IEnumerable<Product> products = pr.GetProduct();
            return View(products);
        }

        public ActionResult Details(int id)
        {
            Product product = pr.GetProduct(id);
            return View(product);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(ViewProductModel product)
        {
            if (ModelState.IsValid)
            {
                data.Id = 0;
                data.Name = product.Name;
                data.Model = product.Model;
                data.Price = product.Price;
                data.Description = product.Description;
                bool result = pr.CreateProduct(data);
            }
            return RedirectToAction("");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product product = pr.GetProduct(id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                bool result = pr.EditProduct(product);
            }
            return RedirectToAction("");
        }
        public ActionResult Delete(int id)
        {
            bool result = pr.DeleteProduct(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

    }
}
