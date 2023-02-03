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

            string htmlContent = "<h1>Données pour l'objet  </h1>" ;
            htmlContent += "<p> Number invoice: " + invoice.InvoiceNumber+ "</p>";

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



    // [HttpGet("generatepdf/Furnisher")]
    // public async Task<IActionResult> GenerateFurnisher(int InvoiceNo)
    // {

    //     // Invoice invoice= JsonConvert.DeserializeObject<Invoice>(File.ReadAllText("../../../InvoiceData.json"));

    //     //Load html HTML template.
    //     //  var invoiceTemplate = File.ReadAllText("../Template/Invoice_Furnisher.html");
    //     //  string invoiceTemplate = File.ReadAllText("../Template/Invoice_Furnisher.html");

    //     // var template = Template.Parse(invoiceTemplate);
    //     // System.IO.File.ReadAllBytes("1.pdf") test jordan garde ca sous la main
    //     // var pdfBytes = _invoiceService.GeneratePdf(InvoiceNo);
    //     // var pdfBytes = _invoiceService.funcForWork("hoh");
    //     // return File(pdfBytes, "application/pdf", "invoice.pdf");

    //      // Générez le contenu HTML à partir du fichier HTML et des données
    // var html = await new ViewRenderService().RenderToStringAsync("NomDuFichierHtml", donnees);

    // // Créez un nouveau PDF à partir du contenu HTML
    // var pdf = new HtmlToPdf();
    // var pdfDocument = pdf.RenderHtmlAsPdf(html);

    // // Renvoyez le fichier PDF dans la réponse
    // return File(pdfDocument.BinaryData, "application/pdf", "FichierPDF.pdf");
    // }

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


