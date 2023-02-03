using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class OfferController : ControllerBase
{
    #region Fields

    private IOfferService _offerService;

    #endregion

    #region Constructors

    public OfferController(IOfferService offerService)
    {
        this._offerService = offerService;
    }

    #endregion

     [HttpGet]
     public async Task<IActionResult> GetAllOffers()
     {
         var offerList = await _offerService.GetAllOffers();
         if (offerList == null)
         {
             return BadRequest();
         }
         else if (offerList.Count == 0)
         {
             return NoContent();
         }
         return Ok(offerList);
     }

     [HttpGet]
     [Route("{id}")]
     public async Task<IActionResult> GetOneOffer(int id)
     {
         var offer = await _offerService.GetOfferById(id);

         if (offer == null)
         {
             return BadRequest();
         }
         return this.Ok(offer);
     }

//Check la disponibilité d'un produit avant d'aller sur la page, route de vérificiation clément
     [HttpGet]
     [Route("checkOfferIsAvailable/{id}")]
     public async Task<IActionResult> checkAvailabilityOfOneOffer(int id)
     {
         var offer = await _offerService.checkAvailabilityOfOneOffer(id);

         return this.Ok(offer);
     }

      [HttpGet]
      [Route("availableOffer")]
      public async Task<IActionResult> GetAvailableOffer()
      {
          var offer = await _offerService.GetAvailableOffers();

        //    if (offer == null)
        //    {
        //        return BadRequest();
        //    }
          return this.Ok(offer);
      }
      [HttpGet]
      [Route("unavailableOffer")]
      public async Task<IActionResult> GetUnavailableOffer()
      {
          var offer = await _offerService.GetUnavailableOffers();

        //    if (offer == null)
        //    {
        //        return BadRequest();
        //    }
          return this.Ok(offer);
      }

     [HttpPost]
     public async Task<IActionResult> AddOne(Offer offer)
     {

         var addedOffer = await _offerService.AddOffer(offer);

         if (addedOffer == null)
         {
             return BadRequest();
         }
         return Ok(addedOffer);
     }

     [HttpPut]
     [Route("{id}")]
     public async Task<IActionResult> UpdateOffer(int id, Offer request)
     {
         var updatedOffer = await _offerService.UpdateOffer(id, request);

         if (updatedOffer == null)
         {
             BadRequest();
         }

         return Ok(updatedOffer);
     }

     [HttpDelete]
     [Route("{id}")]
     public async Task<IActionResult> DeleteOffer(int id)
     {
         var offer = await this._offerService.DeleteOffer(id);

         if (offer == null)
         {
             return BadRequest();
         }

         return Ok(offer);
     }
}

