using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<IActionResult> GetAllFunishers()
    {
        var regionList = await _regionService.GetAllRegions();
        if (regionList == null)
        {
            return BadRequest();
        }
        else if (regionList.Count == 0)
        {
            return NoContent();
        }
        return Ok(regionList);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOneRegion(int id)
    {
        var region = await _regionService.GetOne(id);

        if (region == null)
        {
            return BadRequest();
        }
        return this.Ok(region);
    }

    [HttpPost]
     [Authorize (Roles = "Admin")]
    public async Task<IActionResult> AddOne(RegionRegister request)
    {
        IActionResult result = this.BadRequest();

        var addedRegion = await _regionService.AddRegion(request);

        if (addedRegion == null)
        {
            return BadRequest();
        }
        return Ok(addedRegion);
    }

    [HttpPut]
     [Authorize (Roles = "Admin")]
    [Route("{id}")]

    public async Task<IActionResult> UpdateRegion(int id, RegionRegister request)
    {
        var updatedRegion = await _regionService.UpdateRegion(id, request);

        if (updatedRegion == null)
        {
            BadRequest();
        }

        return Ok(updatedRegion);
    }

    [HttpDelete]
     [Authorize (Roles = "Admin")]
    [Route("{id}")]
    public async Task<IActionResult> DeleteRegion(int id)
    {
        var regionList = await this._regionService.DeleteRegion(id);

        if (regionList == null)
        {
            return BadRequest();
        }

        return Ok(regionList);
    }
}

