using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;

public class ProductOfferService : IProductOfferService
{
    #region Fields
    private readonly DataContext _context;
    #endregion

    // #region Constructors
    public ProductOfferService(DataContext context)
    {
        this._context = context;
    }

     public async Task<List<ProductOffer>?> GetAllProductOffers()
     {
         var offers = await _context.ProductOffers.ToListAsync();
         return offers;
     }

}