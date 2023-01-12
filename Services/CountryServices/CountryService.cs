using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;


namespace GuacAPI.Services;

public class CountryService : ICountryService
{
   #region Fields
   
   private readonly DataContext _context;
   
   #endregion
   
   public CountryService(DataContext context)
   {
      this._context = context;
   }
   
   public async Task<List<Country>> GetAllCountries()
   {
      var countries = await _context.Countries.ToListAsync();
      return countries;
   }
   
   public async Task<Country?> GetOne(int id)
   {
      var country = await _context.Countries.Include(i => i.Products).Where(i => i.CountryId == id).FirstOrDefaultAsync();
      return country;
   }
   
   public async Task<Country> AddCountry(Country country)
   {
      var savedCountry = _context.Countries.Add(country).Entity;
      await _context.SaveChangesAsync();
      return savedCountry;
   }
   
   public async Task<Country?> UpdateCountry(int id, Country request)
   {
      var country = await _context.Countries.FindAsync(id);

      if (country != null)
      {
         country.Name = request.Name;
         
         await _context.SaveChangesAsync();

         return country;
      }

      return null;
   }
   
   public async Task<Country?> DeleteCountry(int id)
   {
      var country = await _context.Countries.FindAsync(id);
      if (country != null)
      {
         _context.Countries.Remove(country);
         
         await _context.SaveChangesAsync();

         return country;
      }

      return null;
   }

   public void SaveChanges()
   {
      this._context.SaveChanges();
   }
}