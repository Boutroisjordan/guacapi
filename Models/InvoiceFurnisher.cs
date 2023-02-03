using GuacAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class InvoiceFurnisher
{
  public int InvoiceFurnisherId { get; set; }
  public string? InvoiceNumber { get; set; }
  public int FurnisherId {get; set;}
  
  
  public Furnisher? Furnisher {get; set;}


  public int InvoiceFurnisherProductId {get; set;}
  // public InvoiceFurnisherProduct? InvoicesFurnisherProduct {get; set;}
  public List<InvoiceFurnisherProduct>? InvoicesFurnisherProduct {get; set;}


}