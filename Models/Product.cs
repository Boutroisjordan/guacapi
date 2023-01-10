namespace GuacAPI.Models;

// Répresente une bouteille de vin
public class Product
{
    #region Properties
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public int Stock {get; set;}
    public decimal AlcoholDegree {get; set;}
    public string? Reference {get; set;}
    public int FurnisherId {get; set;}
    public Furnisher? furnisher;
    public int DomainId {get; set;}
    public Domain? domain;
    public int RegionId {get; set;}
    public Region? region;


    public int MillesimeId {get; set;}
    public Millesime? millesime;
    public int AlcoholTypeId {get; set;}
    public AlcoholType? alcoholType;
    public int AppellationId {get; set;}
    public Appellation? appellation;

    #endregion
}