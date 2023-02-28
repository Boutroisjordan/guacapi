using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;
 
namespace GuacAPI.Services;

public class RegionService : IRegionService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    // #region Constructors
    public RegionService(DataContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }

    public async Task<List<Region>> GetAllRegions()
    {
        var regions = await _context.Regions.ToListAsync();
        return regions;
    }

    public async Task<Region> GetOne(int id)
    {
        var region = await _context.Regions.Include(i => i.Products).Where(i => i.RegionID == id).FirstOrDefaultAsync();
        return region;
    }

    public async Task<Region> AddRegion(RegionRegister request)
    {
        Region region = _mapper.Map<Region>(request);

        var savedRegion = _context.Regions.Add(region).Entity;
        await _context.SaveChangesAsync();

        return savedRegion;
    }


    public async Task<Region> UpdateRegion(int id, RegionRegister request)
    {

        var region = await _context.Regions.FindAsync(id);

        var newRegion = _mapper.Map(request, region);

        if (region != null)
        {

            region.Name = request.Name;


            await _context.SaveChangesAsync();

            return region;
        }

        return null;
    }

    public async Task<List<Region>> DeleteRegion(int id)
    {
        var region = await _context.Regions.FindAsync(id);
        if (region is null)
            return null;

        _context.Regions.Remove(region);

        await _context.SaveChangesAsync();

        return _context.Regions.ToList();
    }

}