using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
namespace GuacAPI.Services;
 
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

    public async Task<List<Appellation>> GetAppellations()
    {
        var appellations = await _context.Appellations.ToListAsync();


        return appellations;
    }

    public async Task<Appellation> GetAppellationById(int id)
    {
        var appellationByid = await _context.Appellations.Include(p => p.Products).Where(a => a.AppellationId == id)
            .FirstOrDefaultAsync();
        if (appellationByid == null)
        {
            return null;
            // throw new Exception("Alcohol not found");
        }

        return appellationByid;
    }

    public async Task<Appellation> GetAppellationByName(string name)
    {
        var appelationName = await _context.Appellations.FirstOrDefaultAsync(a => a.Name == name);
        if (appelationName == null)
        {
            return null;
        }

        return appelationName;
    }

    public async Task<Appellation> CreateAppellation(Appellation appellation)
    {
        if (appellation is null)
        {
            return null;
        }

        var addedAppellation = new Appellation {
            Name = appellation.Name 
        };

        var saveAppellation = _context.Appellations.Add(addedAppellation).Entity;
        await _context.SaveChangesAsync();
        return saveAppellation;
    }


    public async Task<Appellation> UpdateAppellation(int id, Appellation request)
    {

            var appellation = _context.Appellations.Find(id);
            if (appellation == null)
            {
                return null;
            }

            appellation.Name = request.Name;
            await _context.SaveChangesAsync();
            return appellation;
    }

    public async Task<Appellation> DeleteAppellation(int id)
    {
        // return await Task.Run(() =>
        // {
            var appellation = await _context.Appellations.FindAsync(id);
            if (appellation == null)
            {
                throw new ArgumentException("Alcohol not found");
            }

            _context.Appellations.Remove(appellation);
            await _context.SaveChangesAsync();
            return appellation;
        // });
    }

}