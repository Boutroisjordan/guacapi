using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IProductService
{
    //Get all product in Database
    // ICollection<Product> GetAll();

    //Ad one Product in database
    // Product AddOne(Product item);

    ICollection<Product> GetAllProducts();
    Product? GetOne(int id);
    Product AddProduct(Product item);
    Product? UpdateProduct(int id, Product product);
    void SaveChanges();

    // Task<List<Product>> GetAllProducts();
    
    // Product? GetProduct(int id);
    // List<Product> AddProduct(Product product);
    // List<Product> UpdateProduct(int id, Product request);
    // List<Product> DeleteProduct(int id);

}