using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IOfferService
{


     Task<List<Offer>?> GetAllOffers();
     Task<Offer?> GetOfferById(int id);
     Task<Offer> AddOffer(Offer offer);
    //  Task<Product?> UpdateProduct(int id, Product product);
     Task<Offer?> DeleteOffer(int id);
    // void SaveChanges();


}