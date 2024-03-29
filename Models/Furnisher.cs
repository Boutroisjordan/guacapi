using System.ComponentModel.DataAnnotations;

namespace GuacAPI.Models;
 
using System.Text.Json.Serialization;

// fournisseur de vin, revendeur
public class Furnisher
{
    #region Properties
    public int FurnisherId { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string City { get; set; }
    [Required] public string Street { get; set; }
    [Required] public string PostalCode { get; set; }
    [Required] public string Siret { get; set; }

    [JsonIgnore]
    public List<Product> Products { get; set; }
    public List<InvoiceFurnisher> Invoices { get; set; }

    #endregion
}
public class FurnisherRegister
{
    #region Properties
    [Required] public string Name { get; set; }
    [Required] public string City { get; set; }
    [Required] public string Street { get; set; }
    [Required] public string PostalCode { get; set; }
    [Required] public string Siret { get; set; }
    #endregion
}
