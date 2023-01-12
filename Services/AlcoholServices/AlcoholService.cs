using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;

public class AlcoholService : IAlcoholService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    // #region Constructors
    public AlcoholService(DataContext context)
    {
        this._context = context;
    }


    public async Task<List<AlcoholType>?> GetAllTypes()
    {
        var alcohol = await _context.AlcoholTypes.ToListAsync();
        return alcohol;
    }

    public async Task<AlcoholType> GetAlcoholTypeById(int id)
    {
        var alcoholById = await _context.AlcoholTypes.Include(p => p.Products).Where(a => a.AlcoholTypeId == id)
            .FirstOrDefaultAsync();
        if (alcoholById == null)
        {
            throw new Exception("Alcohol not found");
        }

        return alcoholById;
    }

    public async Task<AlcoholType> GetAlcoholByLabel(string label)
    {
        var alcoholName = await _context.AlcoholTypes.FirstOrDefaultAsync(x => x.label == label);
        if (alcoholName == null)
        {
            throw new Exception("Alcohol not found");
        }

        return alcoholName;
    }

    public async Task<AlcoholType?> AddAlcoholType(AlcoholType alcohol)
    {
        if (alcohol is null)
        {
            return null;
        }

        var addedAlcohol = new AlcoholType {
            label = alcohol.label
        };
        

        var savedAlcohol = _context.AlcoholTypes.Add(addedAlcohol).Entity;
        await _context.SaveChangesAsync();
        return savedAlcohol;
    }

    public async Task<AlcoholType?> UpdateAlcoholType(int id, AlcoholType request)
    {

        // return await Task.Run(() =>
        // {}
            var alcohol = await _context.AlcoholTypes.FindAsync(id);
            if (alcohol == null)
            {
                return null;
            }

            alcohol.label = request.label;
            // alcohol.AlcoholTypeId = request.AlcoholTypeId;
            await _context.SaveChangesAsync();
            return alcohol;
    }

    public async Task<AlcoholType?> DeleteAlcoholType(int id)
    {
        // return await Task.Run(() =>
        // {
            var alcohol = await _context.AlcoholTypes.FindAsync(id);
            if (alcohol == null)
            {
                return null;
                // throw new ArgumentException("Alcohol not found");

            }

            _context.AlcoholTypes.Remove(alcohol);
            await _context.SaveChangesAsync();
            return alcohol;
        // });
    }
}