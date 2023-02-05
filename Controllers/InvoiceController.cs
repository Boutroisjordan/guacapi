// using PdfSharpCore;
// using PdfSharpCore.Pdf;
// using TheArtOfDev.HtmlRenderer.PdfSharp;
using GuacAPI.Models;
using GuacAPI.Services;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using AutoMapper;
using GuacAPI.Helpers;
using Swashbuckle.AspNetCore;

 
namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]

public class InvoiceController : ControllerBase
{
    private IInvoiceService _invoiceService;
    private readonly IWebHostEnvironment environnement;
    private readonly IMapper _mapper;

    public InvoiceController(IInvoiceService invoiceService, IWebHostEnvironment webHostEnvironment, IMapper mapper)
    {
        this._invoiceService = invoiceService;
        this.environnement = webHostEnvironment;
        this._mapper = mapper;
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

            // string pathImage = "file:///Users/jordanboutrois/Projects/GuacAPI/GuacAPI/Images/guacalogo.png";
            // string pathImage = "http://" + HttpContext.Request.Host.Value+ "/Images/guacalogo.png";
             string pathImage = "data:image/jpeg;base64, " + await GetBase64string() + "";

            if(invoice.Furnisher is null) {
                return BadRequest("error invoice doesn't have furnisher");
             }
            string htmlContent = "<div style='width:100%'>" ;
            // htmlContent += "<img style='width: 80px; height: 100%' src='" + pathImage + "'  />";
                        // htmlContent += "<img style='width:80px;height:80%' src='" + pathImage + "'   />";
            htmlContent += "<h1>Bon de commande: Réaprovisionnement  </h1>" ;
            htmlContent += "<h1>Date: "+ invoice.Date.ToString("MMMM dd, yyyy") + " </h1>" ;
            // htmlContent += "<p> Number invoice: " + invoice.InvoiceNumber+ "</p>";
            htmlContent += "<p> Furnisher: " + invoice.Furnisher.Name + "</p>";
            htmlContent += "<p> Siret: " + invoice.Furnisher.Siret + "</p>";
            htmlContent += "<p> Adress: " + invoice.Furnisher.Street + " " + invoice.Furnisher.PostalCode + " " + invoice.Furnisher.City+ "</p>";
            // htmlContent += "<p> Date: " + invoice.InvoicesFurnisherProduct + "</p>";
            htmlContent += "</div>";

            // Console.WriteLine(pathImage);

            htmlContent += "<h2> Produits </h2>";

                        htmlContent += "<table style ='width:100%; border: 1px solid #000'>";
            htmlContent += "<thead style='font-weight:bold'>";
            htmlContent += "<tr>";
            htmlContent += "<td style='border:1px solid #000'> Product Name</td>";
            htmlContent += "<td style='border:1px solid #000'> Refrence </td>";
            htmlContent += "<td style='border:1px solid #000'>Qty</td>";

            htmlContent += "</tr>";
            htmlContent += "</thead >";

                        htmlContent += "<tbody>";
    
            if(invoice.InvoicesFurnisherProduct is null) {
                return BadRequest("error invoice doesn't contains products");
             }
            invoice.InvoicesFurnisherProduct.ForEach(item => {
                if(item.Product != null) {




                    htmlContent += "<tr>";
                    htmlContent += "<td>" + item.Product.Name + "</td>";
                    htmlContent += "<td>" + item.Product.Reference + "</td>";
                    htmlContent += "<td>" + item.QuantityProduct + "</td >";
                    htmlContent += "</tr>";
                }
            });

             htmlContent += "<tbody>";
            // Génération du PDF à partir du HTML
            PdfGenerator.AddPdfPages(document, htmlContent, PageSize.A4);
            byte[]? response = null;

            using(MemoryStream ms = new MemoryStream()) {
                document.Save(ms);
                response = ms.ToArray();
            }

            string Filename = "Invoice_" +  invoice.InvoiceFurnisherId +".pdf";

            // Renvoi du PDF en tant que fichier
            return File(response, "application/pdf", Filename);
        }

    [NonAction]
    public async Task<string> GetBase64string() {
        // string filePath= this.environnement.WebRootPath+ "Images/guacalogo.png";
        string filePath= this.environnement.WebRootPath+ "Images/rdh.png";
        // string filePath= "http://" + HttpContext.Request.Host.Value+ "/Images/guacalogo.png";
        byte[] imgarray = await System.IO.File.ReadAllBytesAsync(filePath);
        string base64= Convert.ToBase64String(imgarray);
        return base64;
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
#pragma warning restore CS1591
    /// <summary>
    /// Create a invoice, a command for Furnishers
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Invoice furnisher
    ///     {
    ///         "invoiceNumber" : "string",
    ///         "Date": current datetime already bind,
    ///         "FurnisherId": int,
    ///         "invoicesFurnisherProduct": [
    ///          {
    ///             "productId": int,
    ///             "quantityProduct": int
    ///           }    
    ///         ]
    ///</remarks>
    [HttpPost]
     public async Task<IActionResult> AddOne(InvoiceFurnisherRegister invoice)
     {


        InvoiceFurnisher addedInvoice =  _mapper.Map<InvoiceFurnisher>(invoice);
        
        
         var addedOffer = await _invoiceService.AddInvoice(addedInvoice);

         if (addedOffer == null)
         {
             return BadRequest();
         }
         return Ok(addedOffer);
     }
       
}


