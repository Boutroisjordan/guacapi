using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

namespace GuacAPI.Controllers;
 
[Route("[controller]")]
[ApiController]
public class ProductOfferController : ControllerBase
{
    #region Fields

    private IProductOfferService _productOfferService;

    #endregion

    #region Constructors

    public ProductOfferController(IProductOfferService productOfferService)
    {
        this._productOfferService = productOfferService;
    }

    #endregion

     [HttpGet]
     public async Task<IActionResult> GetAll()
     {
         var offerList =  await _productOfferService.GetAllProductOffers();
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

         public async Task<IActionResult> GetProductOfferById(int id)
     {
         var offerList =  await _productOfferService.GetProductOffersByOfferId(id);
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

}

