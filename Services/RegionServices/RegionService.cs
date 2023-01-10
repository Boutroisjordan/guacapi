using GuacAPI.Models;
using GuacAPI.Context;

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

    public ICollection<Region> GetAllRegions()
    {
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        // var query = from Product in this._context.Products
            var regions =  _context.Regions.ToList();
            return regions;
    }

}