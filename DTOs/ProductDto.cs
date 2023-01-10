namespace GuacAPI.DTOs;

public class ProductDto
{
    #region Properties
        public int Id {get; set;}
        public string? Name {get; set;}
        public string? Category {get; set;}
        public string? Reference {get; set;}
        public decimal Price {get; set;}
        public int FurnisherId {get; set;}
        public int DomainId {get; set;}
        public int RegionId {get; set;}
        public int AlcoholTypeId {get; set;}
    #endregion
}