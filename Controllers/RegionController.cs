using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

using GuacAPI.DTOs;
namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class RegionController : ControllerBase
{
    #region Fields
        private IRegionService _regionService;
    #endregion

    #region Constructors
    public RegionController(IRegionService regionService)
    {
        this._regionService = regionService;
    }
    #endregion


    [HttpGet]
        public IActionResult GetAllFunishers()
        {
            var regionList = this._regionService.GetAllRegions();
            return Ok(regionList);
        }
}

