using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;

namespace GuacAPI.Services;
 
public class FurnisherService : IFurnisherService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    #endregion

    // #region Constructors
    public FurnisherService(DataContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<List<Furnisher>> GetAllFurnishers()
    {
        var furnishers = await _context.Furnishers.ToListAsync();
        return furnishers;
    }

    public async Task<List<Product>> GetProductsOfFurnisher(int id)
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

    public async Task<Furnisher> CreateFurnisher(FurnisherRegister request)
    {

        Furnisher furnisher = _mapper.Map<Furnisher>(request);
        if (furnisher is null)
        {
            throw new Exception("Furnisher is null");
        }

        var savedFurnisher = _context.Furnishers.Add(furnisher).Entity;
        await _context.SaveChangesAsync();
        return savedFurnisher;
    }

    public async Task<Furnisher> UpdateFurnisher(int id, FurnisherRegister request)
    {
        var furnisherToUpdate = await _context.Furnishers.FindAsync(id);
        Furnisher furnisher = _mapper.Map(request, furnisherToUpdate);
        if (furnisherToUpdate is null)
        {
            throw new Exception("Furnisher not found");
        }

        await _context.SaveChangesAsync();
        return furnisherToUpdate;
    }


    public async Task<Furnisher> DeleteFurnisher(int id)
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