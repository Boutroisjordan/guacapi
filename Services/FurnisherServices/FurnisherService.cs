using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;
 
public class FurnisherService : IFurnisherService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    // #region Constructors
    public FurnisherService(DataContext context)
    {
        this._context = context;
    }

    public async Task<List<Furnisher>> GetAllFurnishers()
    {
        var furnishers = await _context.Furnishers.ToListAsync();
        return furnishers;
    }

    public async Task<List<Product>?> GetProductsOfFurnisher(int id)
    {
        var furnisher = await _context.Furnishers.Include(p => p.Products).Where(p => p.FurnisherId == id)
            .FirstOrDefaultAsync();

            if(furnisher is null) {
                return null;
            }

            

        return furnisher.Products;
    }

    public async Task<Furnisher> GetFurnisherById(int id)
    {
        var furnisher = await _context.Furnishers.Include(p => p.Products).Where(p => p.FurnisherId == id)
            .FirstOrDefaultAsync();
        if (furnisher is null)
        {
            throw new Exception("Furnisher not found");
        }

        return furnisher;
    }

    public async Task<Furnisher> GetFurnisherByName(string name)
    {
        var furnisher = await _context.Furnishers.FirstOrDefaultAsync(f => f.Name == name);
        if (furnisher is null)
        {
            throw new Exception("Furnisher not found");
        }

        return furnisher;
    }

    public async Task<Furnisher?> CreateFurnisher(Furnisher furnisher)
    {
        if (furnisher is null)
        {
            throw new Exception("Furnisher is null");
        }

        var addedFurnisher = new Furnisher {
            City = furnisher.City,
            Street = furnisher.Street,
            Name = furnisher.Name,
            PostalCode = furnisher.PostalCode,
            Siret = furnisher.Siret,  
        };

        var savedFurnisher = _context.Furnishers.Add(addedFurnisher).Entity;
        await _context.SaveChangesAsync();
        return savedFurnisher;
    }

    public async Task<Furnisher?> UpdateFurnisher(int id, Furnisher furnisher)
    {
        var furnisherToUpdate = await _context.Furnishers.FindAsync(id);
        if (furnisherToUpdate is null)
        {
            throw new Exception("Furnisher not found");
        }

        furnisherToUpdate.Name = furnisher.Name;
        furnisherToUpdate.City = furnisher.City;
        furnisherToUpdate.Street = furnisher.Street;
        furnisherToUpdate.PostalCode = furnisher.PostalCode;
        furnisherToUpdate.Siret = furnisher.Siret;

        await _context.SaveChangesAsync();
        return furnisherToUpdate;
    }


    public async Task<Furnisher?> DeleteFurnisher(int id)
    {
        var furnisherToDelete = await _context.Furnishers.FindAsync(id);
        if (furnisherToDelete is null)
        {
            throw new Exception("Furnisher not found");
        }

        _context.Furnishers.Remove(furnisherToDelete);
        await _context.SaveChangesAsync();
        return furnisherToDelete;
    }
}