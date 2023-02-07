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
    #endregion


    public InvoiceService(DataContext context, IMapper mapper)
    {
       _context = context;
        this._mapper = mapper;
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

    public async Task<List<InvoiceFurnisher>> GetAllInvoicesFurnisher() {
        var invoices = await _context.InvoicesFurnisher.ToListAsync();

        return invoices;
    }

    public async Task<InvoiceFurnisher> GetInvoiceFurnisher(int id) {
        // var invoice = await _context.InvoicesFurnisher.Include(x => x.InvoicesFurnisherProduct).FirstAsync;
        var invoice = await _context.InvoicesFurnisher
        .Include(x => x.InvoicesFurnisherProduct)
            .ThenInclude(y => y.Product)
        .Include(x => x.Furnisher)
        .Where(x => x.InvoiceFurnisherId == id)
        .FirstOrDefaultAsync();

        if(invoice is null) {
            return null;
        }

        return invoice;
    }

//id en paramètre, et update avec la requete

    public async Task<InvoiceFurnisher> UpdateInvoiceFurnisher(InvoiceFurnisherUpdate request, int id)
    {
        var invoiceFurnisher = await _context.InvoicesFurnisher.FirstOrDefaultAsync(u => u.InvoiceFurnisherId == id);
        if (invoiceFurnisher == null) return null;

        InvoiceFurnisher addedInvoice =  _mapper.Map(request, invoiceFurnisher);


        // user.Username = request.Username;
        await _context.SaveChangesAsync();
        return addedInvoice;
    }

}