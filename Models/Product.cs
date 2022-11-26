namespace GuacAPI.Models;

// Répresente une bouteille de vin
public class Product
{
    #region Properties
    public int ProductId { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
    public string? Reference {get; set;}

    public int FurnisherId {get; set;}
    public Furnisher? furnisher;


    #endregion
}