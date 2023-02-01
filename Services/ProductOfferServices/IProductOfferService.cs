using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IProductOfferService
{


     Task<List<ProductOffer>?> GetAllProductOffers();
     Task<List<ProductOffer>?> GetProductOffersByOfferId(int id);
    //  Task<Offer> AddProductOffer(ProductOffer productOffer);

    // Task<Product?> UpdateProduct(int id, Product product);
    // Task<List<Product>?> DeleteProduct(int id);
    // void SaveChanges();


}