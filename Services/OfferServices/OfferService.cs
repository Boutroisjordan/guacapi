using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using AutoMapper;
using GuacAPI.Helpers;

namespace GuacAPI.Services;

public class OfferService : IOfferService
{
    #region Fields
    private readonly DataContext _context;
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    #endregion

    // #region Constructors
    public OfferService(DataContext context, IProductService productService, IMapper mapper)
    {
        this._context = context;
        this._productService = productService;
        this._mapper = mapper;
    }

    public async Task<List<Offer>> GetAllOffers()
    {
        var offers = await _context.Offers.Include(x => x.ProductOffers)
       .ThenInclude(x => x.Product).ToListAsync();
        return offers;
    }
    public async Task<List<Offer>> GetDraftOffer()
    {
        var offers = await _context.Offers.Where(x => x.isDraft == true).ToListAsync();
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
    public async Task<Boolean> checkAvailabilityOfOneOffer(int id)
    {
        var offer = await _context.Offers
        .Include(o => o.ProductOffers)
        .ThenInclude(x => x.Product)
        .Where(x => x.ProductOffers.Any(item => item.Product != null && (item.Product.Stock - item.QuantityProduct >= 0 || item.Product.RestockOption == true)) == true && x.OfferId == id)
        .Where(x => x.Deadline == null || x.Deadline > DateTime.Now)
        .Where(x => x.isDraft == false)
        .ToListAsync();

        if (offer is null)
        {
            return false;
        }

        return true;
    }
    public async Task<List<Offer>> GetUnavailableOffers()
    {
        var offer = await _context.Offers
        .Include(o => o.ProductOffers)
        .ThenInclude(x => x.Product)
        .Where(x => x.ProductOffers.Any(item => item.Product != null && (item.Product.Stock - item.QuantityProduct >= 0 && item.Product.RestockOption == false)) || x.Deadline < DateTime.Now && x.Deadline != null)
        .ToListAsync();

        if (offer is null)
        {
            throw new Exception("Offer not found man");
        }


        Console.WriteLine(offer.ToString());

        return offer;
    }




    public async Task<Offer> GetOfferById(int id)
    {
        var offer = await _context.Offers.Include(i => i.Comments)
        .Include(o => o.ProductOffers)
         .ThenInclude(x => x.Product)
         .Where(x => x.OfferId == id)
         .FirstOrDefaultAsync();

        if (offer is null)
        {
            throw new Exception("Offer not found");
        }

        return offer;
    }


    public async Task<Offer> AddOffer(OfferRegister request)
    {

        var offer = _mapper.Map<Offer>(request);

        var addedOffer = _context.Offers.Add(offer).Entity;
        await _context.SaveChangesAsync();

        return addedOffer;
    }

    public async Task<Offer> UpdateOffer(int id, OfferRegister request)
    {

         //Trouve l'offre
        var entityOffers = _context.Offers.Include(x => x.ProductOffers).Where(x => x.OfferId == id).FirstOrDefault();

        //Map Update dans une offre
        Offer offer = _mapper.Map(request, entityOffers);
        offer.OfferId = id;

        await _context.SaveChangesAsync();

        return entityOffers;
        // return newOffer;
    }





    public async Task<Offer> DeleteOffer(int id)
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
