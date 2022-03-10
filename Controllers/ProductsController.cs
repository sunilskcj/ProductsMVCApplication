using Microsoft.AspNetCore.Mvc;
using ProductsMVCApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductsMVCApplication.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetProducts()
        {

            ProductRepository repository = new ProductRepository();
            var all = repository.GetAllProducts();
            if(all != null && all.Count > 0) return this.View(all);
            return this.NotFound("No record Found");
        }
        //public ViewResult GetProducts()
        //{
        //    //int x = 10;
        //    //var viewObj = this.ViewBag;
        //    //viewObj.Val = x;

        //    this.ViewBag.Val = 100;
        //    List<Product> productData = ProductRepository.Products;
        //    //ViewData["Products"] = productData;
        //    return this.View(productData);
        //}
        //public IActionResult GetProduct(int arg)
        //{
        //    var allProducts = ProductRepository.Products;

        //    var product = allProducts.Where(p => p.ProductId == arg);

        //    if (product != null && product.Count() > 0)
        //        return this.View(product.First());
        //    else 
        //        return this.NotFound("No records Found");

        //}
        public IActionResult GetProduct(int arg)
        {
            ProductRepository repository = new ProductRepository();
            Product all = repository.GetARecordById(arg);
            if (all == null ) return this.NotFound("No record Found"); 
            else return this.View(all);

        }


        //[HttpGet]
        //public IActionResult AddProduct()
        //{
        //    return this.View();
        //}

        //[HttpPost]
        //public IActionResult AddProduct(Product newProductData)
        //{
        //   var allProducts = ProductRepository.Products; // getting all products from the repository
        //    var prod = allProducts.Where(p => p.ProductId == newProductData.ProductId);
        //    if (prod != null && prod.Count() > 0)
        //    {
        //        this.ViewBag.err = "Product already exists";
        //        return this.View();
        //    }
        //    else ProductRepository.Add(newProductData);

        //    return this.Redirect("GetProducts");

        //}

        [HttpGet]
        public IActionResult AddProduct()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product newProductData)
        {
            ProductRepository repository = new ProductRepository();
            try
            {
                bool status = repository.AddProduct(newProductData);
                if (!status)
                {
                    this.ViewBag.err = "Product not added";
                }
                return this.Redirect("GetProducts");
            }
            catch (Exception ex)
            {
                this.ViewBag.err = ex.Message;
                return this.View();
            }

           

        }
        [HttpGet]
        public IActionResult DeleteProduct()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult DeleteProduct(int ProductID)
        {
            ProductRepository repository = new ProductRepository();
            bool status = repository.DeleteProduct(ProductID);
            if (!status)
            {
                this.ViewBag.err = "err";
            }
            return this.Redirect("GetProducts");

        }

    }
}
