using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IProductService
{


    Task<List<Product>?> GetAllProducts();
    Task<Product?> GetOne(int id);
    Task<Product> AddProduct(Product item);
    Task<Product?> UpdateProduct(int id, Product product);
    Task<List<Product>?> DeleteProduct(int id);
    Task<int> CheckStock(int id);
}