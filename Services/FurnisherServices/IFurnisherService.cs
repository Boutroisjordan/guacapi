using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IFurnisherService
{
    //Get all product in Database
    // ICollection<Product> GetAll();

    //Ad one Product in database
    // Product AddOne(Product item);

     ICollection<Furnisher> GetAllFurnishers();
      //  Product? GetOne(int id);
    //  Product AddOne(Product item);
    // Task<List<Product>> GetAllProducts();
    
    // Product? GetProduct(int id);
    // List<Product> AddProduct(Product product);
    // List<Product> UpdateProduct(int id, Product request);
    // List<Product> DeleteProduct(int id);

}