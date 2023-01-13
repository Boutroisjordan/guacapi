using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;

public class OfferService : IOfferService
{
    #region Fields
    private readonly DataContext _context;
    #endregion

    // #region Constructors
    public OfferService(DataContext context)
    {
        this._context = context;
    }

    public async Task<List<Offer>?> GetAllOffers()
    {
        var offers = await _context.Offers.ToListAsync();
        return offers;
    }

    public async Task<Offer?> GetOfferById(int id)
    {
        // var offer = await _context.Furnishers.Include(p => p.Products).Where(p => p.FurnisherId == id).FirstOrDefaultAsync();
        var offer = await _context.Offers.Include(p => p.ProductOffers).Where(p => p.OfferId == id).FirstOrDefaultAsync();
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
        // await _context.SaveChangesAsync();

        if (offer.ProductOffers != null)
        {

            foreach (var productOffer in offer.ProductOffers)
            {
                //pendant le get, tu vérifie le nombe de fois ou tu peux acheter l'offre  et tu n'affiche pas ceux qui ne dispose plus de produit disponible
                //  var stock


                productOffer.OfferId = addedOffer.OfferId;
                _context.ProductOffers.Add(productOffer);
            }
            await _context.SaveChangesAsync();
        }
        // Ajouter les ProductOffer à la base de données avec l'id de l'offre qui vient d'être créée

        return addedOffer;
    }

    // public async Task<Product?> UpdateProduct(int id, Product request)
    // {

    //     var product = await _context.Products.FindAsync(id);

    //     if (product != null)
    //     {

    //         product.Name = request.Name;
    //         product.Price = request.Price;
    //         product.Stock = request.Stock;
    //         product.Millesime = request.Millesime;
    //         product.AlcoholDegree = request.AlcoholDegree;
    //         product.AlcoholTypeId = request.AlcoholTypeId;
    //         product.Reference = request.Reference;
    //         product.FurnisherId = request.FurnisherId;
    //         product.DomainId = request.DomainId;
    //         product.RegionId = request.RegionId;
    //         product.AppellationId = request.AppellationId;


    //         await _context.SaveChangesAsync();

    //         return product;
    //     }

    //     return null;
    // }

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