using GuacAPI.Models;

namespace GuacAPI.Services;

public interface IOfferService
{


     Task<List<Offer>?> GetAllOffers();
     Task<Offer?> GetOfferById(int id);
     Task<Offer> AddOffer(Offer offer);
      Task<Offer?> UpdateOffer(int id, Offer offer);
     Task<Offer?> DeleteOffer(int id);
    // void SaveChanges();

     Task<List<Offer>> GetAvailableOffers();
     Task<List<Offer>> GetUnavailableOffers();


}