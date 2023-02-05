using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IInvoiceService
{

string funcForWork(string ehoh);

Task<InvoiceFurnisher?> GeneratePDF(int invoiceId);

    Task<InvoiceFurnisher> AddInvoice(InvoiceFurnisher invoice);

    Task<List<InvoiceFurnisher>> GetAllInvoicesFurnisher();
    Task<InvoiceFurnisher?> GetInvoiceFurnisher(int id);

}