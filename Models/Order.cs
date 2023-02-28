using GuacAPI.Models;
using GuacAPI.Entities;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
 


public class Order
{
  public int OrderId { get; set; }
  public int UserId {get; set;}
  public int Total {get; set;}
  public User user {get; set;}
  public List<OrderOffer> OrderOffers {get; set;}
  public DateTime orderedAt {get; set;} = DateTime.Today;
  public int OrderStatusId {get; set;}
  public OrderStatus OrderStatus {get; set;}

}
public class OrderRegistryDTO
{
  public int UserId {get; set;}
  public List<OrderOfferRegistryDTO> OrderOfferRegistryDTOs {get; set;}
  public DateTime orderedAt {get; set;} = DateTime.Today;
  public int OrderStatusId {get; set;}

}
public class OrderUpdateDTO
{
  public List<OrderOfferRegistryDTO> OrderOfferRegistryDTOs {get; set;}
  public DateTime orderedAt {get; set;} = DateTime.Today;
  public int OrderStatusId {get; set;}

}

