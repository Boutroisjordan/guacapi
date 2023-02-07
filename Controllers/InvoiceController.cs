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
    private IInvoiceServiceProduct _invoiceServiceProduct;
    private readonly IMapper _mapper;

    public InvoiceController(IInvoiceService invoiceService, IInvoiceServiceProduct invoiceServiceProduct, IMapper mapper)
    {
        this._invoiceService = invoiceService;
        this._invoiceServiceProduct = invoiceServiceProduct;
        this._mapper = mapper;
    }

    [HttpGet("pdf")]
    public async Task<IActionResult> GetPdf(int id)
    {
        // Générez le PDF

        var document = new PdfDocument();
        var invoice = await _invoiceService.GeneratePDF(id);

        if (invoice is null)
        {
            return BadRequest("error invoice not found");
        }

        if (invoice.Furnisher is null)
        {
            return BadRequest("error invoice doesn't have furnisher");
        }
        string htmlContent = "<div style='width:100%'>";
        htmlContent += "<h1>Bon de commande: Réaprovisionnement  </h1>";
        htmlContent += "<h1>Date: " + invoice.Date.ToString("MMMM dd, yyyy") + " </h1>";
        htmlContent += "<p> Furnisher: " + invoice.Furnisher.Name + "</p>";
        htmlContent += "<p> Siret: " + invoice.Furnisher.Siret + "</p>";
        htmlContent += "<p> Adress: " + invoice.Furnisher.Street + " " + invoice.Furnisher.PostalCode + " " + invoice.Furnisher.City + "</p>";
        htmlContent += "</div>";
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

        if (invoice.InvoicesFurnisherProduct is null)
        {
            return BadRequest("error invoice doesn't contains products");
        }
        invoice.InvoicesFurnisherProduct.ForEach(item =>
        {
            if (item.Product != null)
            {
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

        using (MemoryStream ms = new MemoryStream())
        {
            document.Save(ms);
            response = ms.ToArray();
        }

        string Filename = "Invoice_" + invoice.InvoiceFurnisherId + ".pdf";

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


    [HttpGet("invoiceProduct")]
    public async Task<IActionResult> GetAll()
    {

        var addedOffer = await _invoiceServiceProduct.getAll();

        if (addedOffer == null)
        {
            return BadRequest();
        }
        return Ok(addedOffer);
    }

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
    ///       }    
    ///</remarks>
    [HttpPost]
    public async Task<IActionResult> AddOne(InvoiceFurnisherRegister invoice)
    {

        var addedOffer = await _invoiceService.AddInvoice(invoice);

        if (addedOffer == null)
        {
            return BadRequest();
        }
        return Ok(addedOffer);
    }


    /// <summary>
    /// Create a invoiceProduct, InvoiceProduct
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Invoice furnisher
    ///     {
    ///           "quantityProduct": 340,
    ///           "invoiceFurnisherId": 19,
    ///           "productId": 1
    ///       }    
    ///</remarks>
    [HttpPost("invoiceProduct")]
    public async Task<IActionResult> AddProductInvoice(InvoiceFurnisherProduct invoice)
    {

        var addedOffer = await _invoiceServiceProduct.AddInvoiceProduct(invoice);
        if (addedOffer == null)
        {
            return BadRequest();
        }
        return Ok(addedOffer);
    }


    /// <summary>
    /// Create a invoiceProduct, InvoiceProduct
    /// </summary>
    /// <remarks>
    /// Tu ne peux pas modifier les deux ID parce que cela fait partis de leur primary key.
    /// Tu peux en revanche changer la quantité ou supprimer le produit.
    ///
    ///</remarks>
    [HttpPut("invoiceProduct/{invoiceFurnisherId}/{productId}")]
    public async Task<IActionResult> EditProductInvoice(int productId, int invoiceFurnisherId, InvoiceFurnisherProduct invoice)
    {

        var addedOffer = await _invoiceServiceProduct.EditInvoiceProduct(productId, invoiceFurnisherId, invoice);
        if (addedOffer == null)
        {
            return BadRequest();
        }
        return Ok(addedOffer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, InvoiceFurnisherUpdate invoice)
    {

        var addedInvoice = await _invoiceService.UpdateInvoiceFurnisher(invoice, id);

        if (addedInvoice == null)
        {
            return BadRequest();
        }
        return Ok(addedInvoice);
    }


    [HttpDelete("{invoiceFurnisherId}/{productId}")]
    public async Task<IActionResult> DeleteProductInvoice(int productId, int invoiceFurnisherId)
    {

        var deletedOffer = await _invoiceServiceProduct.DeleteInvoiceProduct(productId, invoiceFurnisherId);
        if (deletedOffer == null)
        {
            return BadRequest();
        }
        return Ok(deletedOffer);
    }

}


