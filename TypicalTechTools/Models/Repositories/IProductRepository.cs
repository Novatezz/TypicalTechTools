namespace TypicalTechTools.Models.Repositories
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProductById(int id);
        void createProduct(Product product);
        void updateProduct(Product product);
        void deleteProduct(int id);
    }
}
