namespace GuacAPI.Models;
using System.Text.Json.Serialization;

// Répresente une bouteille de vin
public class Millesime
{
    #region Properties
    public int MillesimeId { get; set; }
    public int Year { get; set; }

    public List<Product>? Products {get; set;}

    #endregion
}