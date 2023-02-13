using GuacAPI.Models;
using GuacAPI.Entities;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
 


public class OrderStatus
{
  public int OrderStatusId { get; set; }
  public string OrderStatusName {get; set;}
  public List<Order> Orders {get; set;}

}

