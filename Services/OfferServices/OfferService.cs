using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;

public class OfferService : IOfferService
{
    #region Fields
    private readonly DataContext _context;
    private readonly IProductService _productService;
    #endregion

    // #region Constructors
    public OfferService(DataContext context, IProductService productService)
    {
        this._context = context;
        this._productService = productService;
    }

    public async Task<List<Offer>?> GetAllOffers()
    {
        var offers = await _context.Offers.ToListAsync();
        return offers;
    }

    public async Task<List<Offer>> GetAvailableOffers()
    {
        var offer = await _context.Offers
        .Include(o => o.ProductOffers)
        .ThenInclude(x => x.Product)
        .Where(x => x.ProductOffers.Any(item => item.Product != null && item.Product.Stock - item.QuantityProduct >= 0) == true)
        .ToListAsync();

        if (offer is null)
        {
            throw new Exception("Offer not found man");
        }


        Console.WriteLine(offer.ToString());

        return offer;
    }
    public async Task<List<Offer>> GetUnavailableOffers()
    {
        var offer = await _context.Offers
        .Include(o => o.ProductOffers)
        .ThenInclude(x => x.Product)
        .Where(x => x.ProductOffers.Any(item => item.Product != null && item.Product.Stock - item.QuantityProduct >= 0) == false)
        .ToListAsync();

        if (offer is null)
        {
            throw new Exception("Offer not found man");
        }


        Console.WriteLine(offer.ToString());

        return offer;
    }




    public async Task<Offer?> GetOfferById(int id)
    {
        var offer = await _context.Offers.Include(o => o.ProductOffers)
         .ThenInclude(x => x.Product)
         .Where(x => x.OfferId == id)
         .FirstOrDefaultAsync();

        if (offer is null)
        {
            throw new Exception("Offer not found");
        }

        return offer;
    }

    //  public async Task<Offer> AddOffer(Offer request)
    //  {

    //      var newOffer = new Offer
    //         {
    //             Name = request.Name,
    //             Description = request.Description,
    //             Price = request.Price
    //         };
    //         var saveOffer = _context.Offers.Add(newOffer).Entity;
    //         await _context.SaveChangesAsync();

    //         if(request.ProductOffers != null)
    //         {
    //             var newProductOffer = new ProductOffer
    //             {
    //                 OfferId = saveOffer.OfferId,
    //                 ProductId = request.ProductOffers[0].ProductId
    //             };

    //             await _context.SaveChangesAsync();
    //         }


    //      return saveOffer;
    //  }


    public async Task<Offer> AddOffer(Offer offer)
    {


        // Ajouter l'offre à la base de données
        var addedOffer = _context.Offers.Add(offer).Entity;


        if (offer.ProductOffers != null)
        {

            foreach (var productOffer in offer.ProductOffers)
            {
                productOffer.OfferId = addedOffer.OfferId;
                _context.ProductOffers.Add(productOffer);
            }
            await _context.SaveChangesAsync();
        }


        return addedOffer;
    }

     public async Task<Offer?> UpdateOffer(int id, Offer request)
     {

         var offer = await _context.Offers.FindAsync(id);

         if (offer != null)
         {

             offer.Name = request.Name;
             offer.Description = request.Description;
             offer.Price = request.Price;
             offer.ImageUrl = request.ImageUrl;
             offer.ProductOffers = request.ProductOffers;

            foreach (var productOffer in offer.ProductOffers)
            {
                var productOfferUp = await _context.ProductOffers.FindAsync(productOffer.OfferId, productOffer.ProductId);
                _context.ProductOffers.Update(productOffer);
                
            }

             await _context.SaveChangesAsync();

             return offer;
         }

         return null;
     }

    public async Task<Offer?> DeleteOffer(int id)
    {

        var offer = await _context.Offers.Include(x => x.ProductOffers).FirstOrDefaultAsync(x => x.OfferId == id);
        if (offer == null)
            return null;

        if (offer.ProductOffers != null)
            // Supprimer les ProductOffers associés à cette offre
            _context.ProductOffers.RemoveRange(offer.ProductOffers);

        _context.Offers.Remove(offer);

        await _context.SaveChangesAsync();

        return offer;
    }

}