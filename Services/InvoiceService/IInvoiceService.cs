using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IInvoiceService
{
    Task<InvoiceFurnisher?> GeneratePDF(int invoiceId);
    Task<InvoiceFurnisher> AddInvoice(InvoiceFurnisherRegister invoice);
    Task<List<InvoiceFurnisher>> GetAllInvoicesFurnisher();
    Task<InvoiceFurnisher?> GetInvoiceFurnisher(int id);
    Task<InvoiceFurnisher?> UpdateInvoiceFurnisher(InvoiceFurnisherUpdate request, int id);

}