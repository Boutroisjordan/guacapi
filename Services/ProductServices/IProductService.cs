using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IProductService
{

    Task<List<Product>> GetAllProducts();
    Task<Product> GetOne(int id);
    Task<Product> GetByName(string name);
    Task<Product> AddProduct(ProductRegister item);
    Task<Product> UpdateProduct(int id, ProductRegister product);
    Task<List<Product>> DeleteProduct(int id);
    Task<int> CheckStock(int id);
}