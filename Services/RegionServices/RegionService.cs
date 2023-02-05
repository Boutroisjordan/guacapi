using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
 
namespace GuacAPI.Services;

public class RegionService : IRegionService
{
    #region Fields
    private readonly DataContext _context;
    #endregion

    // #region Constructors
    public RegionService(DataContext context)
    {
        this._context = context;
    }

    public async Task<List<Region>> GetAllRegions()
    {
        var regions = await _context.Regions.ToListAsync();
        return regions;
    }
    //-------
    public async Task<Region?> GetOne(int id)
    {
        var region = await _context.Regions.Include(i => i.Products).Where(i => i.RegionID == id).FirstOrDefaultAsync();
        return region;
    }

    public async Task<Region> AddRegion(Region region)
    {
        var savedRegion = _context.Regions.Add(region).Entity;
        await _context.SaveChangesAsync();

        return savedRegion;
    }

    public void SaveChanges()
    {
        this._context.SaveChanges();
    }

    public async Task<Region?> UpdateRegion(int id, Region request)
    {

        var region = await _context.Regions.FindAsync(id);

        if (region != null)
        {

            region.Name = request.Name;


            await _context.SaveChangesAsync();

            return region;
        }

        return null;
    }

    public async Task<List<Region>?> DeleteRegion(int id)
    {
        var region = await _context.Regions.FindAsync(id);
        if (region is null)
            return null;

        _context.Regions.Remove(region);

        await _context.SaveChangesAsync();

        return _context.Regions.ToList();
    }

}