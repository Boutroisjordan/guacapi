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

    // public async Task<List<Product>> GetAllProducts()
    // {
    //     //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
    //     // var query = from Product in this._context.Products
    //         var products = await _context.Products;
    //         return products ;
    // }
    public async Task<ICollection<AlcoholType>> GetAllTypes()
    {
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        // var query = from Product in this._context.Products
        var alcohol = await _context.AlcoholTypes.ToListAsync();
        return alcohol;
    }

    public async Task<AlcoholType> GetAlcoholTypeById(int id)
    {
        var alcoholById = await _context.AlcoholTypes.FindAsync(id);
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

    public async Task<AlcoholType> AddAlcoholType(AlcoholType alcohol)
    {
        if (alcohol is null)
        {
            throw new ArgumentNullException(nameof(alcohol));
        }

        _context.AlcoholTypes.Add(alcohol);
        await _context.SaveChangesAsync();
        return alcohol;
    }

    public async Task<AlcoholType> UpdateAlcoholType(int id, AlcoholType request)
    {
        return await Task.Run(() =>
        {
            var alcohol = _context.AlcoholTypes.Find(id);
            if (alcohol == null)
            {
                throw new ArgumentException("Alcohol not found");
            }

            alcohol.label = request.label;
            alcohol.AlcoholTypeId = request.AlcoholTypeId;
            return alcohol;
        });
    }

    public async Task<AlcoholType> DeleteAlcoholType(int id)
    {
        return await Task.Run(() =>
        {
            var alcohol = _context.AlcoholTypes.Find(id);
            if (alcohol == null)
            {
                throw new ArgumentException("Alcohol not found");
            }

            _context.AlcoholTypes.Remove(alcohol);
            _context.SaveChanges();
            return alcohol;
        });
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}