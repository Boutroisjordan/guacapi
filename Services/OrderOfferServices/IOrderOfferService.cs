using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IOrderOfferService
{
    Task<OrderOffer> Add(OrderOfferRegistryDTO request);
    Task<List<OrderOffer>> getAll();
    
    // Task<OrderOffer> EditInvoiceProduct(int id, int invoiceFurnisherId, InvoiceFurnisherProduct invoice);
    // Task<InvoiceFurnisherProduct> DeleteInvoiceProduct(int id, int invoiceFurnisherId);

}