using ApiLayer.Models;
using BusinessObjectLayer;
using DomainLayer;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiLayer.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILog _log;
        ProductDbContext _context;
        IProductCatalog _catalog;
        ResponseModel<Product> _response;
        IEnumerable<Product> _productList;
        Product _product;
        ResponseMethod<Product> _responseMethod;
        /*IRepositoryOperations<Product> _repo;*/
        public ProductController(ProductDbContext context, IProductCatalog catalog)
        {
            _context = context;
            _catalog = catalog;
            _response = new ResponseModel<Product>();
            _product = new Product();
            _responseMethod = new ResponseMethod<Product>();
            _log = LogManager.GetLogger(typeof(ProductController));

        }
        [HttpPost]
        public ResponseModel<Product> Post([FromBody] Product product)
        {
            try
            {
                    _catalog.AddProduct(product);
                    string message = "succesfully added" + ", Response Message : " + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                _response = _responseMethod.AddResponse(false, 0, null, null, message);
                    return _response;
            }
            catch (Exception ex)
            {
                
                string message = "Unable to add product " + ", Response Message : " + new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                _response=_responseMethod.AddResponse(true, 0, null, null, message);
                _log.Error("log4net : error in the post controller",ex);
                return _response;
            }

        }
        [HttpPut]
        public ResponseModel<Product> Put([FromBody] Product product)
        {
            try
            {
                _catalog.EditProduct(product);
                string message = "Succesfully edited "+ new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                _responseMethod.AddResponse(false, 0, null, null, message);
                return _response;
            }
            catch (Exception ex)
            {
                string message = "Unable to edit "+ new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                _responseMethod.AddResponse(true, 0, null, null, message);
                _log.Error("log4net : error in the post controller", ex);
                return _response;
            }
        }
        [HttpGet]
        public ResponseModel<Product> Get()
        {
            try
            {
                _productList = _catalog.GetProduct();
                if (_productList == null)
                {
                    string message = "Products Does not exist" + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    _response = _responseMethod.AddResponse(false, 0, _productList, null, message);
                    return _response;
                }
                else
                {
                    string message = "" + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    _response = _responseMethod.AddResponse(false, 0, _productList, null, message);
                    
                    return _response;
                }
                
            }
            catch(Exception ex)
            {
                _productList = _catalog.GetProduct();
                string message = "" + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                _response = _responseMethod.AddResponse(true, 0, null, null, message);
                _log.Error("log4net : error in the post controller", ex);
                return _response;
            }
           
        }
        [HttpGet("{id}")]
        public ResponseModel<Product> Get(int id)
        {
            try
            {
                _product = _catalog.GetById(id);
                if(_product == null)
                {
                    string message = "Product does not exist" + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    _response = _responseMethod.AddResponse(false, 0, null, _product, message);
                    return _response;
                }
                else
                {
                    string message = "" + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                    _response = _responseMethod.AddResponse(false, 0, null, _product, message);
                    return _response;
                }
                
            }
            catch(Exception ex)
            {
                string message = "" + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                _response = _responseMethod.AddResponse(false, 0, null, null, message);
                _log.Error("log4net : error in the post controller", ex);
                return _response;
            }
            
        }
        [HttpDelete("{id}")]
        public ResponseModel<Product> Delete(int id)
        {
            try
            {
                _product = _catalog.GetById(id);
                _catalog.DeleteProduct(_product);
                string message = "Succefully Deleted" + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                _response = _responseMethod.AddResponse(false, 0, null, null, message);
                return _response;
            }
            catch(Exception ex)
            {
                string message = "Product Does not exist" + new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                _response = _responseMethod.AddResponse(true, 0, null, null, message);
                _log.Error("log4net : error in the post controller", ex);
                return _response;
            }
        }
    }
}
