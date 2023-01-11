using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

// Répresente une bouteille de vin
public class Product
{
    #region Properties

    public int ProductId { get; set; }
<<<<<<< HEAD
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public int Stock {get; set;}
    public string? Millesime { get; set; }
    public decimal AlcoholDegree {get; set;}
    public string? Reference {get; set;}
    public int FurnisherId {get; set;}
=======
    [Required] public string? Name { get; set; }
    [Required] public decimal Price { get; set; }
    [Required] public int Stock { get; set; }
    public int Millesime { get; set; }
    [Required] public decimal AlcoholDegree { get; set; }
    [Required] public string? Reference { get; set; }
    public int FurnisherId { get; set; }
>>>>>>> origin/featuresAddedCrud
    public Furnisher? furnisher;
    public int DomainId { get; set; }
    public Domain? domain;
    public int RegionId { get; set; }
    public Region? region;
    public int AlcoholTypeId { get; set; }
    public AlcoholType? alcoholType;
    public int AppellationId { get; set; }
    public Appellation? appellation;

    #endregion
}