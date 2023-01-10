using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

using GuacAPI.DTOs;
namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class FurnisherController : ControllerBase
{
    #region Fields
        private IFurnisherService _furnisherService;
    #endregion

    #region Constructors
    public FurnisherController(IFurnisherService furnisherService)
    {
        this._furnisherService = furnisherService;
    }
    #endregion


    [HttpGet]
        public IActionResult GetAllFunishers()
        {
            var furnisherList = this._furnisherService.GetAllFurnishers();
            return Ok(furnisherList);
        }

        //  public ActionResult<Product> GetOne(int id)
        //  {
        //      var product = _productService.GetOne(id);
        //      return Ok(product);
        //  }

    
      /*  [HttpGet]
        [Route("{id}")]
         public IActionResult GetOneProduct(int id)
         {

            var product = _productService.GetOne(id);

            return this.Ok(product);
         } */
}

