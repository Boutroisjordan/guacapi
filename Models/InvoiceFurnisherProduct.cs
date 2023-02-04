using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;
using System.Text.Json.Serialization;

// table de relation
public class InvoiceFurnisherProduct

{
    #region Properties
    public int Id {get; set;} //il sert a rien dégage le et test mais tu dois wipe la bdd
    public int QuantityProduct {get; set;}



    public int InvoiceFurnisherId {get; set;}
    public InvoiceFurnisher? InvoiceFurnisher {get; set;}

    public int ProductId { get; set; }
    public Product? Product { get; set; }


    #endregion
}