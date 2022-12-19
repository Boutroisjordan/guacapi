namespace GuacAPI.DTOs;

public class ProductDto
{
    #region Properties
        public int Id {get; set;}
        public string? Name {get; set;}
        public string? Category {get; set;}
        public decimal Price {get; set;}
        public int FurnisherId {get; set;}
    #endregion
}