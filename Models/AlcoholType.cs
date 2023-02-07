using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;
 
using System.Text.Json.Serialization;

// la couleur du vin (rouge, blanc etc)
public class AlcoholType
{
    #region Properties

    public int AlcoholTypeId { get; set; }
    [Required] public string label { get; set; }
    public List<Product> Products { get; set; }

    #endregion
}