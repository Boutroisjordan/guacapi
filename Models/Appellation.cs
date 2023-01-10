namespace GuacAPI.Models;
using System.Text.Json.Serialization;

//  AOP IGP etc
public class Appellation
{
    #region Properties
    public int AppellationId { get; set; }
    public string? Name { get; set;}

    public List<Product>? Products {get; set;}

    #endregion
}