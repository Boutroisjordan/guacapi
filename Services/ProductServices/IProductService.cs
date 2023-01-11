using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IProductService
{
    //Get all product in Database
    // ICollection<Product> GetAll();

    //Ad one Product in database
    // Product AddOne(Product item);

    Task<List<Product>?> GetAllProducts();
    Task<Product?> GetOne(int id);
    Task<Product> AddProduct(Product item);
    Task<Product?> UpdateProduct(int id, Product product);
    Task<List<Product>?> DeleteProduct(int id);
    void SaveChanges();



    // Task<List<Product>> GetAllProducts();
    
    // Product? GetProduct(int id);
    // List<Product> AddProduct(Product product);
    // List<Product> UpdateProduct(int id, Product request);
    // List<Product> DeleteProduct(int id);

}