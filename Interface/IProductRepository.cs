using GuacAPI.Models;

namespace GuacAPI.Interface;

public interface IProductRepository : IRepository
{

    //Get all product in Database
    ICollection<Product> GetAll();

    //Ad one Product in database
    Product AddOne(Product item);
}