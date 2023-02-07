using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

using System.Text.Json.Serialization;

 
public class Offer

{
    #region Properties

    public int OfferId { get; set; }
    public string Name {get; set;}
    public string Description {get; set;}
    public double Price {get; set;}
    public string ImageUrl {get; set;}

    // public ICollection<Product>? Products {get; set;}

    // [JsonIgnore]
     public List<ProductOffer> ProductOffers {get; set;} = new List<ProductOffer>();

    #endregion

}