// using PdfSharpCore;
// using PdfSharpCore.Pdf;
// using TheArtOfDev.HtmlRenderer.PdfSharp;
using GuacAPI.Models;
using GuacAPI.Services;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]

public class InvoiceController : ControllerBase
{
    private IInvoiceService _invoiceService;

    public InvoiceController(IInvoiceService invoiceService)
    {
        this._invoiceService = invoiceService;
    }

        [HttpGet("pdf")]
        public async Task<IActionResult> GetPdf(int id)
        {
        // Générez le PDF

            var document = new PdfDocument();
             var invoice = await _invoiceService.GeneratePDF(id);

             if(invoice is null) {
                return BadRequest("error invoice not found");
             }


            if(invoice.Furnisher is null) {
                return BadRequest("error invoice doesn't have furnisher");
             }
            string htmlContent = "<h1>Bon de commande: Réaprovisionnement  </h1>" ;
            htmlContent += "<p> Number invoice: " + invoice.InvoiceNumber+ "</p>";
            htmlContent += "<p> Furnisher: " + invoice.Furnisher.Name + "</p>";
            htmlContent += "<p> Siret: " + invoice.Furnisher.Siret + "</p>";
            htmlContent += "<p> address: " + invoice.Furnisher.Street + " " + invoice.Furnisher.PostalCode + " " + invoice.Furnisher.City+ "</p>";

            htmlContent += "<h2> Produits </h2>";
    
            if(invoice.InvoicesFurnisherProduct is null) {
                return BadRequest("error invoice doesn't contains products");
             }
            invoice.InvoicesFurnisherProduct.ForEach(item => {
                if(item.Product != null) {

                htmlContent += "<p>Nom du produit : " + item.Product.Name + "</p>";
                htmlContent += "<p>Quantité : " + item.QuantityProduct + "</p>";
                }
            });
            // Génération du PDF à partir du HTML
            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
            byte[]? response = null;

            using(MemoryStream ms = new MemoryStream()) {
                document.Save(ms);
                response = ms.ToArray();
            }

            string Filename = "Invoice_test.pdf";

            // Renvoi du PDF en tant que fichier
            return File(response, "application/pdf", Filename);
        }

    [HttpGet]
     public async Task<IActionResult> getAll()
     {

         var invoices = await _invoiceService.GetAllInvoicesFurnisher();

         if (invoices == null)
         {
             return BadRequest();
         }
         return Ok(invoices);
     }
    [HttpGet("{id}")]
     public async Task<IActionResult> getOne(int id)
     {

         var invoices = await _invoiceService.GetInvoiceFurnisher(id);

         if (invoices == null)
         {
             return BadRequest();
         }
         return Ok(invoices);
     }
    [HttpPost]
     public async Task<IActionResult> AddOne(InvoiceFurnisher invoice)
     {

         var addedOffer = await _invoiceService.AddInvoice(invoice);

         if (addedOffer == null)
         {
             return BadRequest();
         }
         return Ok(addedOffer);
     }
       
}


