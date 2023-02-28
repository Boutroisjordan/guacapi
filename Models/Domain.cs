namespace GuacAPI.Models;
using System.Text.Json.Serialization;
 

public class Domain
{
    #region Properties
    public int DomainId { get; set; }
    public string Name { get; set; }
    public List<Product> Products {get; set;}
    
    #endregion
}
public class DomainRegister
{
    #region Properties
    public string Name { get; set; }
      
    #endregion
}