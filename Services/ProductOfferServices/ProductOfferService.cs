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

     public async Task<List<ProductOffer>> GetAllProductOffers()
     {
         var offers = await _context.ProductOffers.ToListAsync();
         return offers;
     }
     public async Task<List<ProductOffer>> GetProductOffersByOfferId(int id)
     {
         var offers = await _context.ProductOffers.Include(p => p.Product).Where(p => p.OfferId == id).ToListAsync();
        //  var offers = await _context.ProductOffers.Where(p => p.OfferId == id).ToListAsync();
         return offers;
     }

//TODO l'édit des productOffer comme invoice furnisher 
         public async Task<ProductOffer> EditProductOffer(int id, int OfferId, ProductOffer request)
    {
        // Ajouter l'offre à la base de données
        var addedInvoice = await _context.InvoicesFurnisherProduct.FirstOrDefaultAsync(x => x.ProductId == id && x.InvoiceFurnisherId == invoiceFurnisherId);

        if (addedInvoice is null)
        {
            throw new Exception("invoice product");
        }
        addedInvoice.InvoiceFurnisherId = invoice.InvoiceFurnisherId;
        addedInvoice.ProductId = invoice.ProductId;
        addedInvoice.QuantityProduct = invoice.QuantityProduct;
    
        await _context.SaveChangesAsync();

        return addedInvoice;
    }


}