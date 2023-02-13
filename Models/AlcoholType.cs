using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;
 
using System.Text.Json.Serialization;


public class AlcoholType
{
    #region Properties

    public int AlcoholTypeId { get; set; }
    [Required] public string label { get; set; }
    public List<Product> Products { get; set; }

    #endregion
}
public class AlcoholTypeRegister
{
    #region Properties
    [Required] public string label { get; set; }
    #endregion
}