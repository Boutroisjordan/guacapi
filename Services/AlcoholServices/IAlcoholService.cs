using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IAlcoholService

{
    //Get all product in Database
    // ICollection<Product> GetAll();

    //Ad one Product in database
    // Product AddOne(Product item);

    Task<List<AlcoholType>?> GetAllTypes();
    Task<AlcoholType> GetAlcoholTypeById(int id);
    Task<AlcoholType> GetAlcoholByLabel(string label);

    Task<AlcoholType?> AddAlcoholType(AlcoholType alcohol);

    Task<AlcoholType?> UpdateAlcoholType(int id, AlcoholType alcohol);
    Task<AlcoholType?> DeleteAlcoholType(int id);
}