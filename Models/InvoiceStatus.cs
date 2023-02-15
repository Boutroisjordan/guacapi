using GuacAPI.Models;
using GuacAPI.Entities;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
 


public class InvoiceStatus
{
  public int InvoiceStatusId { get; set; }
  public string InvoiceStatusName {get; set;}

  public List<Order> Invoices {get; set;}

}

