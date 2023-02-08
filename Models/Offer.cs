using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

using System.Text.Json.Serialization;

 
public class Offer

{
    #region Properties

    //TODO images

    public int OfferId { get; set; }
    public string Name {get; set;}
    public string Description {get; set;}
    public double Price {get; set;}
    public string ImageUrl {get; set;}
    public DateTime? Deadline {get; set;} = null;
    public bool isB2B {get; set;} = false;

    // public ICollection<Product>? Products {get; set;}

    // [JsonIgnore]
     public List<ProductOffer> ProductOffers {get; set;} = new List<ProductOffer>();
     public List<Comment> Comments {get; set;}

    #endregion
}