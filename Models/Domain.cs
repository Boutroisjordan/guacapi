namespace GuacAPI.Models;
using System.Text.Json.Serialization;

// Le domiane qui l'a produit
public class Domain
{
    #region Properties
    public int DomainId { get; set; }
    public string? Name { get; set; }
    public List<Product>? Products {get; set;}
    
    public  Domain()
    {
        Products = new List<Product>();
    }
    
    #endregion
}