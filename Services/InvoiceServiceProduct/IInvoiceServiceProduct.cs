using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IInvoiceServiceProduct
{
    Task<InvoiceFurnisherProduct> AddInvoiceProduct(InvoiceFurnisherProduct invoice);
    Task<List<InvoiceFurnisherProduct>> getAll();
    Task<InvoiceFurnisherProduct> EditInvoiceProduct(int id, int invoiceFurnisherId, InvoiceFurnisherProduct invoice);
    Task<InvoiceFurnisherProduct?> DeleteInvoiceProduct(int id, int invoiceFurnisherId);

}