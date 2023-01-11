using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services.AppellationServices;

public class AppellationService : IAppellationService
{
    #region Fields

    private readonly DataContext _context;

    #endregion

    // #region Constructors
    public AppellationService(DataContext context)
    {
        this._context = context;
    }

    public async Task<ICollection<Appellation>> GetAppellations()
    {
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        // var query = from Product in this._context.Products
        var appellations = await _context.Appellations.ToListAsync();
        return appellations;
    }

    public async Task<Appellation> GetAppellationById(int id)
    {
        var appellationByid = await _context.Appellations.Include(p => p.Products).Where(a => a.AppellationId == id)
            .FirstOrDefaultAsync();
        if (appellationByid == null)
        {
            throw new Exception("Alcohol not found");
        }

        return appellationByid;
    }

    public async Task<Appellation> GetAppellationByName(string name)
    {
        var appelationName = await _context.Appellations.FirstOrDefaultAsync(a => a.Name == name);
        if (appelationName == null)
        {
            throw new Exception("Alcohol not found");
        }

        return appelationName;
    }

    public async Task<Appellation> CreateAppellation(Appellation appellation)
    {
        if (appellation is null)
        {
            throw new ArgumentNullException(nameof(appellation));
        }

        _context.Appellations.Add(appellation);
        await _context.SaveChangesAsync();
        return appellation;
    }


    public async Task<Appellation> UpdateAppellation(int id, Appellation request)
    {
        return await Task.Run(() =>
        {
            var appellation = _context.Appellations.Find(id);
            if (appellation == null)
            {
                throw new ArgumentException("Alcohol not found");
            }

            appellation.Name = request.Name;
            return appellation;
        });
    }

    public async Task<Appellation> DeleteAppellation(int id)
    {
        return await Task.Run(() =>
        {
            var appellation = _context.Appellations.Find(id);
            if (appellation == null)
            {
                throw new ArgumentException("Alcohol not found");
            }

            _context.Appellations.Remove(appellation);
            _context.SaveChanges();
            return appellation;
        });
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}