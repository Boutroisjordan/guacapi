using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;
using GuacAPI.Models;



namespace GuacAPI.Services;

public class OrderOfferService : IOrderOfferService
{
    #region Fields
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    #endregion

    public OrderOfferService(DataContext context, IMapper mapper)
    {
        _context = context;
        this._mapper = mapper;
    }

    public async Task<List<OrderOffer>> getAll()
    {
        // Ajouter l'offre à la base de données
        var result = await _context.OrderOffers.ToListAsync();

        if (result is null)
        {
            throw new Exception("no one find");
        }

        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<OrderOffer> Add(OrderOfferRegistryDTO request)
    {
        // Ajouter l'offre à la base de données

        OrderOffer orderOffer = _mapper.Map<OrderOffer>(request);

        var addedInvoice = await _context.OrderOffers.AddAsync(orderOffer);

        await _context.SaveChangesAsync();
        return addedInvoice.Entity;
    }
    public async Task<InvoiceFurnisherProduct> EditInvoiceProduct(int id, int invoiceFurnisherId, InvoiceFurnisherProduct invoice)
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

    public async Task<InvoiceFurnisherProduct> DeleteInvoiceProduct(int id, int invoiceFurnisherId)
    {
        var deletedInvoice = await _context.InvoicesFurnisherProduct.FirstOrDefaultAsync(x => x.ProductId == id && x.InvoiceFurnisherId == invoiceFurnisherId);
        if (deletedInvoice is null)
        {
            return null;
        }

        _context.InvoicesFurnisherProduct.Remove(deletedInvoice);
        await _context.SaveChangesAsync();

        return deletedInvoice;
    }

}