using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;
using System.Text.Json.Serialization;
 
// table de relation
public class InvoiceFurnisherProduct

{
    #region Properties
    public int QuantityProduct {get; set;}

    public int InvoiceFurnisherId {get; set;}
    [JsonIgnore]
    public InvoiceFurnisher InvoiceFurnisher {get; set;}

    public int ProductId { get; set; }
    public Product Product { get; set; }


    #endregion
}

