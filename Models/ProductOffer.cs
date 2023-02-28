using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;
using System.Text.Json.Serialization;

 
public class ProductOffer

{
    #region Properties

    // public int ProductOfferId {get; set;}
    public int QuantityProduct {get; set;}
    public int ProductId { get; set; }
    // [JsonIgnore]
    public Product Product { get; set; }

    public int OfferId {get; set;}
    [JsonIgnore]
    public Offer Offer {get; set;}

    #endregion
}
public class ProductOfferRegister

{
    #region Properties

    public int QuantityProduct {get; set;}
    public int ProductId { get; set; }
    public int OfferId {get; set;}
    #endregion
}