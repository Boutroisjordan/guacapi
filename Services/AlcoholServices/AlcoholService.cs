using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;

namespace GuacAPI.Services;
 
public class AlcoholService : IAlcoholService
{
    #region Fields

    private readonly DataContext _context;
    private readonly IMapper _mapper;

    #endregion

    // #region Constructors
    public AlcoholService(DataContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
    }


    public async Task<List<AlcoholType>> GetAllTypes()
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

    public async Task<AlcoholType> AddAlcoholType(AlcoholTypeRegister alcohol)
    {
        if (alcohol is null)
        {
            return null;
        }

        AlcoholType alcoholType = _mapper.Map<AlcoholType>(alcohol);
        

        var savedAlcohol = _context.AlcoholTypes.Add(alcoholType).Entity;
        await _context.SaveChangesAsync();
        return savedAlcohol;
    }

    public async Task<AlcoholType> UpdateAlcoholType(int id, AlcoholTypeRegister request)
    {

            var alcohol = await _context.AlcoholTypes.FindAsync(id);
            if (alcohol == null)
            {
                throw new Exception("No AlcholType with this id exist");
            }

            AlcoholType alcoholType = _mapper.Map(request, alcohol);
            await _context.SaveChangesAsync();
            return alcohol;
    }

    public async Task<AlcoholType> DeleteAlcoholType(int id)
    {

            var alcohol = await _context.AlcoholTypes.FindAsync(id);
            if (alcohol == null)
            {
                throw new ArgumentException("Alcohol not found");
            }
            _context.AlcoholTypes.Remove(alcohol);
            await _context.SaveChangesAsync();
            return alcohol;
    }
}