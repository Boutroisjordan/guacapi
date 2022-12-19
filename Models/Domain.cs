namespace GuacAPI.Models;
using System.Text.Json.Serialization;

// Répresente une bouteille de vin
public class Domain
{
    #region Properties
    public int DomainId { get; set; }
    public string? Name { get; set; }


    public List<Product>? Products {get; set;}

    #endregion
}