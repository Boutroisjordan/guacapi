using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

// pays d'où vient le vin
public class Country
{
    #region Properties
    
    public int CountryId { get; set; }
    
    [Required] public string? Name { get; set; }
    
    public List<Product>? Products { get; set; }
    
    #endregion
}