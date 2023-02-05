using GuacAPI.Context;
// using Microsoft.AspNetCore.Routing.Template;
// using DinkToPdf.Contracts;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using AutoMapper;
using GuacAPI.Helpers;
using GuacAPI.Models;

 

namespace GuacAPI.Services;

public class InvoiceServiceProduct : IInvoiceServiceProduct
{
    #region Fields
    private readonly DataContext _context;
        private readonly IMapper _mapper;
    // private readonly RazorLightEngine _engine;
    #endregion


    public InvoiceServiceProduct(DataContext context, IMapper mapper)
    {
       _context = context;
        this._mapper = mapper;
        // _engine = new RazorLightEngineBuilder().UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates")).Build();
    }

   

       public async Task<List<InvoiceFurnisherProduct>> getAll() {
                // Ajouter l'offre à la base de données
        var invoice = await _context.InvoicesFurnisherProduct.ToListAsync();
        
        if(invoice is null) {
            throw new Exception("no one find");
        }

        await _context.SaveChangesAsync();


        return invoice;
        }

       public async Task<InvoiceFurnisherProduct> AddInvoiceProduct(InvoiceFurnisherProduct invoice) {
                // Ajouter l'offre à la base de données
        var addedInvoice = await _context.InvoicesFurnisherProduct.AddAsync(invoice);

        await _context.SaveChangesAsync();


        return addedInvoice.Entity;
        }
       public async Task<InvoiceFurnisherProduct> EditInvoiceProduct(int id, int invoiceFurnisherId, InvoiceFurnisherProduct invoice) {
                // Ajouter l'offre à la base de données
        var addedInvoice = await _context.InvoicesFurnisherProduct.FirstOrDefaultAsync(x => x.ProductId == id && x.InvoiceFurnisherId == invoiceFurnisherId);
        
        if (addedInvoice is null) {
            throw new Exception("invoice product");
        }
        addedInvoice.InvoiceFurnisherId = invoice.InvoiceFurnisherId;
        addedInvoice.ProductId = invoice.ProductId;
        addedInvoice.QuantityProduct = invoice.QuantityProduct;
        // var addedInvoice = await _context.InvoicesFurnisherProduct.AddAsync(invoice);

        await _context.SaveChangesAsync();


        return addedInvoice;
        }

        public async Task<InvoiceFurnisherProduct?> DeleteInvoiceProduct(int id, int invoiceFurnisherId)
    {
               var deletedInvoice = await _context.InvoicesFurnisherProduct.FirstOrDefaultAsync(x => x.ProductId == id && x.InvoiceFurnisherId == invoiceFurnisherId);

               if(deletedInvoice is null) {
                return null;
               }

        _context.InvoicesFurnisherProduct.Remove(deletedInvoice);

        await _context.SaveChangesAsync();

        return deletedInvoice;
    }

}