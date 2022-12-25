namespace GuacAPI.Models;
using System.Text.Json.Serialization;

// Région d'ou vient le vin
public class Region
{
    #region Properties
    public int RegionID { get; set; }
    public string? Name { get; set;}

    public List<Product>? Products {get; set;}

    #endregion
}