namespace GuacAPI.Models;
using System.Text.Json.Serialization;

// Répresente une bouteille de vin
public class AlcoholType
{
    #region Properties
    public int AlcoholTypeId { get; set; }
    public string? code_type { get; set; }
    public string? label  { get; set; }
    public List<Product>? Products {get; set;}


    #endregion
}