namespace GuacAPI.Models;
using System.Text.Json.Serialization;

// Répresente une bouteille de vin
public class Furnisher
{
    #region Properties
    public int FurnisherId { get; set; }
    public string? Name { get; set; }
    public string? City { get; set;}
    public string? Street { get; set;}
    public string? PostalCode { get; set;}
    public string? Siret { get; set;}


    public List<Product>? Products {get; set;}

    #endregion
}