
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TypicalTechTools.Models.Data;

namespace TypicalTechTools.Models.Repositories
{
    public class ProductRepository : IProductRepository
    {
        // Database context for accessing Product data
        private readonly TechToolsDBContext _context;

        // Constructor to initialize the repository with the database context
        public ProductRepository(TechToolsDBContext context) 
        {
            _context = context;
        }

        // Method to create and save a new product
        public void createProduct(Product product)
        {
            // Add the new product to the Products DbSet
            _context.Products.Add(product);
            // Save changes to the database
            _context.SaveChanges();
        }

        // Method to delete a product with a given ID
        public void deleteProduct(int id)
        {
            // Find the product with the specified ID and remove it from the DbSet
            //ExecuteDelete() is used for direct database deletion
            _context.Products.Where(p => p.Id == id).ExecuteDelete();  
        }

        // Method to retrieve all products from the database
        public List<Product> GetAllProducts()
        {
            // Retrieve all products and convert to a list
            return _context.Products.ToList();
        }

        // Method to retrieve a single product by its ID
        public Product GetProductById(int id)
        {
            // Find the product with the specified ID or return null if not found
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        // Method to update an existing product
        public void updateProduct(Product product)
        {
            // Mark the product entity as modified
            _context.Products.Update(product);
            // Save changes to the database
            _context.SaveChanges();
        }
    }
}
