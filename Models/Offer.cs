using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

using System.Text.Json.Serialization;

// la couleur du vin (rouge, blanc etc)
public class Offer

{
    #region Properties

    public int OfferId { get; set; }
    public string? Name {get; set;}
    public string? Description {get; set;}
    public double Price {get; set;}
    public int Quantity {get; set;}
    public string? ImageUrl {get; set;}

    // public ICollection<Product>? Products {get; set;}

    // [JsonIgnore]
     public List<ProductOffer>? ProductOffers {get; set;}

    #endregion

    // https://www.youtube.com/watch?v=FHx6AGVF_IE patrick god
}