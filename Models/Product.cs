using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GuacAPI.Models;

 
public class Product
{
    #region Properties
    public int ProductId { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string ImageUrl { get; set; }
    [Required] public int Price { get; set; }
    [Required] public int Stock { get; set; }
    public int Millesime { get; set; }
    [Required] public float AlcoholDegree { get; set; }
    [Required] public string Reference { get; set; }
    public int FurnisherId { get; set; }
    public Furnisher furnisher;
    public int DomainId {get; set;}
    public Domain domain;
    public int RegionId {get; set;}
    public Region region;
    public int AlcoholTypeId {get; set;}
    public AlcoholType alcoholType;
    public int AppellationId {get; set;}
    public Appellation appellation;

    // public ICollection<Offer> Offers {get; set;}

    [JsonIgnore]
     public List<ProductOffer> ProductOffers {get; set;}

    //   public List<InvoiceFurnisher> InvoiceFurnishers {get; set;}
      public List<InvoiceFurnisherProduct> InvoicesFurnisherProduct {get; set;}

    #endregion
}

public class ProductRegister 
{
    [Required] public string Name { get; set; }
    [Required] public string ImageUrl { get; set; }
    [Required] public int Price { get; set; }
    [Required] public int Stock { get; set; }
    [Required] public int Millesime { get; set; }
    [Required] public float AlcoholDegree { get; set; }
    [Required] public string Reference { get; set; }
     [Required]public int FurnisherId { get; set; }
     [Required] public int DomainId {get; set;}
     [Required] public int RegionId {get; set;}
     [Required] public int AlcoholTypeId {get; set;}
     [Required] public int AppellationId {get; set;}

}