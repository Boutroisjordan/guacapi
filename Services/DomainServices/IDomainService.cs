using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IDomainService
{
    //Get all product in Database
    // ICollection<Product> GetAll();

    //Ad one Product in database
    // Product AddOne(Product item);

    Task<ICollection<Domain>> GetAllDomains();
    Task<Domain?> GetDomainById(int id);
    Task<Domain?> GetDomainByName(string name);

    Task<Domain?> AddDomain(Domain domain);

    Task<Domain?> UpdateDomain(int id, Domain domain);
    Task<Domain?> DeleteDomain(int id);

    void SaveChanges();

    //  Product? GetOne(int id);
    //  Product AddOne(Product item);
    // Task<List<Product>> GetAllProducts();

    // Product? GetProduct(int id);
    // List<Product> AddProduct(Product product);
    // List<Product> UpdateProduct(int id, Product request);
    // List<Product> DeleteProduct(int id);
}