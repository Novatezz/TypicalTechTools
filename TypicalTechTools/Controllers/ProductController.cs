using TypicalTechTools.DataAccess;
using TypicalTechTools.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TypicalTechTools.Models.Repositories;

namespace TypicalTools.Controllers
{
    public class ProductController : Controller
    {
        // Repository interface for product operations
        private readonly IProductRepository _repository;

        // Constructor to inject the product repository
        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: /Product/Index
        // Displays a list of all products
        [HttpGet]
        public ActionResult Index()
        {
            // Retrieve all products from the repository
            var products = _repository.GetAllProducts();
            // Return the Index view with the product list
            return View(products);
        }

        // GET: /Product/AddProduct
        // Displays the form for adding a new product
        public ActionResult AddProduct()
        {
            return View();
        }

        // POST: /Product/AddProduct
        // Handles the form submission for adding a new product
        [HttpPost]
        public ActionResult AddProduct(Product product) 
        {
            try
            {
                // Check if the provided product data is valid
                if (ModelState.IsValid == true)
                {
                    // Store the session ID in session data (for tracking purposes)
                    HttpContext.Session.SetString("SessionId", HttpContext.Session.Id);

                    // Add the session ID and update date to the product being saved
                    product.SessionId = HttpContext.Session.Id;
                    product.UpdatedDate = DateTime.Now;

                    // Send the product data to the repository for saving
                    _repository.createProduct(product);
                    // Redirect to the Index page after successful addition
                    return RedirectToAction(nameof(Index));
                }
                // If the data is not valid, return the form view with the product data
                return View(product);
            }
            catch
            {
                // If an error occurs, return the form view with the product data
                return View(product);
            }
        }

        // GET: /Product/EditProduct/{id}
        // Displays the form for editing an existing product
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            // Retrieve the product by ID
            Product product = _repository.GetProductById(id);
            // Return the EditProduct view with the product data
            return View(product);
        }

        // POST: /Product/EditProduct
        // Handles the form submission for updating an existing product
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            try {
                // Check if the provided product data is valid
                if (ModelState.IsValid == true)
                {
                    // Store the session ID in session data (for tracking purposes)
                    HttpContext.Session.SetString("SessionId", HttpContext.Session.Id);

                    // Add the session ID and update date to the product being updated
                    product.SessionId = HttpContext.Session.Id;
                    product.UpdatedDate = DateTime.Now;

                    // Send the updated product data to the repository
                    _repository.updateProduct(product);
                    // Redirect to the Index page after successful update
                    return RedirectToAction(nameof(Index));
                }
                // If the data is not valid, return the form view with the product data
                return View(product);
            }
            catch {
                // If an error occurs, return the form view with the product data
                return View(product); 
            }
        }

        // GET: /Product/Privacy
        // Displays the Privacy Policy view 
        public ActionResult Privacy()
        {
            return View();
        }
    }
}
