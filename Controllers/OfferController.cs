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

    // [HttpPut]
    // [Route("{id}")]
    // public async Task<IActionResult> UpdateRegion(int id, Region request)
    // {
    //     var updatedRegion = await _regionService.UpdateRegion(id, request);

    //     if (updatedRegion == null)
    //     {
    //         BadRequest();
    //     }

    //     return Ok(updatedRegion);
    // }

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

