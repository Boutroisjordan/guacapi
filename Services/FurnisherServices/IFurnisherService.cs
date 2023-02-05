using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IFurnisherService
{
    //Get all product in Database
    Task<List<Furnisher>> GetAllFurnishers();

    //Get furnisher by Id
    Task<Furnisher> GetFurnisherById(int id);

    Task<List<Product>?> GetProductsOfFurnisher(int id);

    // get furnisher by name
    Task<Furnisher> GetFurnisherByName(string name);

//Add new furnisher
    Task<Furnisher?> CreateFurnisher(Furnisher furnisher);

    //Update furnisher
    Task<Furnisher?> UpdateFurnisher(int id, Furnisher furnisher);

    //Delete furnisher
    Task<Furnisher?> DeleteFurnisher(int id);
}