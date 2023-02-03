using GuacAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class InvoiceFurnisher
{
  public int InvoiceFurnisherId { get; set; }
  public string? InvoiceNumber { get; set; }
  public int FurnisherId {get; set;}
  
  
  public Furnisher? Furnisher {get; set;}
//   public UserDetails? CustomerDetails { get; set; }

  // public List<Product>? Products { get; set; }

//   public decimal SubTotal
//   {
//     get
//     {
//       return Items.Sum(x => x.Price * x.Quantity);
//     }
//   }

//   public float Tax
//   {
//     get
//     {
//        return (float)SubTotal * (25f / 100f);
//     }
//   }

//   public float GrandTotal
//   {
//     get
//     {
//       return (float)SubTotal + Tax;
//     }
//   }
    public List<InvoiceFurnisherProduct>? InvoicesFurnisherProduct {get; set;}
    // public List<Product>? Products {get; set;}

}