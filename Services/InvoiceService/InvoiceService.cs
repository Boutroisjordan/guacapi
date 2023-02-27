using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;

namespace GuacAPI.Services;

public class InvoiceService : IInvoiceService
{
    #region Fields
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IInvoiceServiceProduct _invoiceServiceProduct;
    #endregion


    public InvoiceService(DataContext context, IMapper mapper, IInvoiceServiceProduct invoiceServiceProduct)
    {
        _context = context;
        this._mapper = mapper;
        this._invoiceServiceProduct = invoiceServiceProduct;
    }

    public async Task<InvoiceFurnisher> GeneratePDF(int invoiceId)
    {

        var invoice = await _context.InvoicesFurnisher
        .Include(x => x.InvoicesFurnisherProduct)
            .ThenInclude(y => y.Product)
        .Include(x => x.Furnisher)
        .Where(x => x.InvoiceFurnisherId == invoiceId)
        .FirstOrDefaultAsync();
        return invoice;
    }

    public async Task<InvoiceFurnisher> AddInvoice(InvoiceFurnisherRegister request)
    {
        InvoiceFurnisher invoice = _mapper.Map<InvoiceFurnisher>(request);
        // Ajouter l'offre à la base de données
        var addedInvoice = await _context.InvoicesFurnisher.AddAsync(invoice);

        await _context.SaveChangesAsync();

        return addedInvoice.Entity;
    }

    public async Task<List<InvoiceFurnisher>> GetAllInvoicesFurnisher()
    {
        var invoices = await _context.InvoicesFurnisher.Include(x => x.InvoicesFurnisherProduct).ThenInclude(x => x.Product).Include(x => x.Furnisher).ToListAsync();

        return invoices;
    }

    public async Task<InvoiceFurnisher> GetInvoiceFurnisher(int id)
    {
        // var invoice = await _context.InvoicesFurnisher.Include(x => x.InvoicesFurnisherProduct).FirstAsync;
        var invoice = await _context.InvoicesFurnisher
        .Include(x => x.InvoicesFurnisherProduct)
            .ThenInclude(y => y.Product)
        .Include(x => x.Furnisher)
        .Where(x => x.InvoiceFurnisherId == id)
        .FirstOrDefaultAsync();

        if (invoice is null)
        {
            return null;
        }

        return invoice;
    }


    public async Task<InvoiceFurnisher> UpdateInvoiceFurnisher(InvoiceFurnisherUpdate request, int id)
    {
        //Récupérer la facture
        var entityInvoice = await _context.InvoicesFurnisher.Include(x => x.InvoicesFurnisherProduct).Where(x => x.InvoiceFurnisherId == id).FirstOrDefaultAsync();

        //Map OBJ Update dans une facture
        InvoiceFurnisher invoiceFurnisher = _mapper.Map(request, entityInvoice);
        invoiceFurnisher.InvoiceFurnisherId = id;


    //sauvegarder
        await _context.SaveChangesAsync();

        return invoiceFurnisher;
    }

    public async Task<InvoiceFurnisher> ChangeStatus(int id, int StatusId) {
        var invoice = await _context.InvoicesFurnisher.Include(x => x.InvoicesFurnisherProduct).ThenInclude(y => y.Product).Where(x => x.InvoiceFurnisherId == id).FirstOrDefaultAsync();
        
        invoice.InvoicesFurnisherProduct.ForEach(itemProduct => {
            var product = _context.Products.Find(itemProduct.ProductId);

            if(product is null) {
                throw new Exception("Product doesn't find");
            }

            product.Stock += itemProduct.QuantityProduct;
        });

        return invoice;
    }

}