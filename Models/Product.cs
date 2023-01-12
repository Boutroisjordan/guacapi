using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

// Répresente une bouteille de vin
public class Product
{
    #region Properties
    public int ProductId { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public decimal Price { get; set; }
    [Required] public int Stock { get; set; }
    public int Millesime { get; set; }
    [Required] public decimal AlcoholDegree { get; set; }
    [Required] public string? Reference { get; set; }
    public int FurnisherId { get; set; }
    public Furnisher? furnisher;
    public int DomainId {get; set;}
    public Domain? domain;
    public int RegionId {get; set;}
    public Region? region;
    public int CountryId {get; set;}
    public Country? country;
    public int AlcoholTypeId {get; set;}
    public AlcoholType? alcoholType;
    public int AppellationId {get; set;}
    public Appellation? appellation;

    #endregion
}