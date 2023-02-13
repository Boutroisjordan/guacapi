using GuacAPI.Models;
using GuacAPI.Entities;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
 


public class OrderOffer
{
  public int Quantity {get; set;}
  public int OrderId { get; set; }
  public Order order {get; set;}
  public int OfferId {get; set;}
  public Offer offer {get; set;}

}
public class OrderOfferRegistryDTO
{
  public int OrderId {get; set;}
  public int OfferId {get; set;}
  public int Quantity {get; set;}

}

