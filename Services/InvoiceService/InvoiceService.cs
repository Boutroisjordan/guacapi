using GuacAPI.Context;
// using Microsoft.AspNetCore.Routing.Template;
// using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;


namespace GuacAPI.Services;

public class InvoiceService : IInvoiceService
{
    #region Fields
    private readonly DataContext _context;
    // private readonly RazorLightEngine _engine;
    #endregion


    public InvoiceService(DataContext context)
    {
       _context = context;
        // _engine = new RazorLightEngineBuilder().UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates")).Build();
    }

    public string funcForWork(string ehoh) {
        return ehoh;
    }

        public async Task<InvoiceFurnisher?> GeneratePDF(int invoiceId)
        {

            //  var Getinvoice = await _context.InvoiceFurnisher.FirstOrDefaultAsync(x => x.Id == invoiceId);
             var invoice = await _context.InvoicesFurnisher.Include(x => x.Furnisher).FirstOrDefaultAsync(x => x.InvoiceFurnisherId == invoiceId);

            return invoice;
        }

        //dto pour pas prendre l'id
    public async Task<InvoiceFurnisher> AddInvoice(InvoiceFurnisher invoice) 
    {
        // Ajouter l'offre à la base de données
        var addedInvoice = await _context.InvoicesFurnisher.AddAsync(invoice);

        await _context.SaveChangesAsync();

        

            // foreach (var product in _context.InvoicesFurnisherProduct)
            // {
            //     product.InvoiceFurnisherId = addedInvoice.Entity.InvoiceFurnisherId;
            //     _context.InvoicesFurnisherProduct.Add(product);

            //     //todo get all product of furnisher and check dans command si les produit sont de fournisseur rentrer sinon error merci
            // }

            await _context.SaveChangesAsync();

    
        return addedInvoice.Entity;
    }


    //     public async Task<Offer> AddOffer(Offer offer)
    // {


    //     // Ajouter l'offre à la base de données
    //     var addedOffer = _context.Offers.Add(offer).Entity;


    //     if (offer.ProductOffers != null)
    //     {

    //         foreach (var productOffer in offer.ProductOffers)
    //         {
    //             productOffer.OfferId = addedOffer.OfferId;
    //             _context.ProductOffers.Add(productOffer);
    //         }
    //         await _context.SaveChangesAsync();
    //     }


    //     return addedOffer;
    // }

    public async Task<List<InvoiceFurnisher>> GetAllInvoicesFurnisher() {
        var invoices = await _context.InvoicesFurnisher.ToListAsync();

        return invoices;
    }

    public async Task<InvoiceFurnisher?> GetInvoiceFurnisher(int id) {
        // var invoice = await _context.InvoicesFurnisher.Include(x => x.InvoicesFurnisherProduct).FirstAsync;
        var invoice = await _context.InvoicesFurnisher.FindAsync(id);

        if(invoice is null) {
            return null;
        }

        return invoice;
    }
}