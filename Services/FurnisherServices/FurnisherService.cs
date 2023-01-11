using GuacAPI.Models;
using GuacAPI.Context;

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


    public ICollection<Furnisher> GetAllFurnishers()
    {
            var furnishers =  _context.Furnishers.ToList();
            return furnishers;
    }

}