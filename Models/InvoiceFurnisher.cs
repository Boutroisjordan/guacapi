using GuacAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
 
public class InvoiceFurnisher
{
  public int InvoiceFurnisherId { get; set; }
  public string? InvoiceNumber { get; set; }

  public DateTime Date {get; set;} = DateTime.Today;

  public int FurnisherId {get; set;}
  public Furnisher? Furnisher {get; set;}


  // public int InvoiceFurnisherProductId {get; set;}
  // public InvoiceFurnisherProduct? InvoicesFurnisherProduct {get; set;}
  // public List<Product>? Products {get; set;}
  public List<InvoiceFurnisherProduct> InvoicesFurnisherProduct {get; set;} = new List<InvoiceFurnisherProduct>();


}

public class InvoiceFurnisherRegister {

  public string? invoiceNumber {get; set;}

  public DateTime Date {get; set;} = DateTime.Today;

  public int FurnisherId {get; set;}

  public List<InvoiceFurnisherProduct> InvoicesFurnisherProduct {get; set;} = new List<InvoiceFurnisherProduct>();
}


public class InvoiceFurnisherUpdate {

  public string? invoiceNumber {get; set;}

  public DateTime Date {get; set;} = DateTime.Today;

  public int FurnisherId {get; set;}

  // public List<InvoiceFurnisherProduct> InvoicesFurnisherProduct {get; set;} = new List<InvoiceFurnisherProduct>();
}