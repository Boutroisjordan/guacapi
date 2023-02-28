using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;

using System.Text.Json.Serialization;

 
public class Region
{
    #region Properties

    public int RegionID { get; set; }
    [Required] public string Name { get; set; }

    public List<Product> Products { get; set; }

    #endregion
}
public class RegionRegister
{
    #region Properties

    [Required] public string Name { get; set; }

    #endregion
}