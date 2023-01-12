using GuacAPI.Models;

namespace GuacAPI.Services;

public interface ICountryService
{
    Task<List<Country>> GetAllCountries();
    Task<Country?> GetOne(int id);
    Task<Country> AddCountry(Country country);
    Task<Country?> UpdateCountry(int id, Country request);
    Task<Country?> DeleteCountry(int id);
    
    void SaveChanges();
}