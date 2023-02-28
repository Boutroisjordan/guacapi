using GuacAPI.Models;

namespace GuacAPI.Services;
 
public interface IOfferService
{


     Task<List<Offer>> GetAllOffers();
     Task<Offer> GetOfferById(int id);
     Task<List<Offer>> GetDraftOffer();
     Task<Offer> AddOffer(OfferRegister offer);
      Task<Offer> UpdateOffer(int id, OfferRegister offer);
     Task<Offer> DeleteOffer(int id);

     Task<List<Offer>> GetAvailableOffers();
     Task<List<Offer>> GetUnavailableOffers();

     Task<Boolean> checkAvailabilityOfOneOffer(int id);


}