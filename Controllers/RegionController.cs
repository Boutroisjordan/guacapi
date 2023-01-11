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
    public async Task<IActionResult> GetAllFunishers()
    {
        var regionList = await _regionService.GetAllRegions();
    if(regionList == null)
     {
        return BadRequest();
     }
        return Ok(regionList);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOneRegion(int id)
    {
        var region = await _regionService.GetOne(id);

        if(region == null)
        {
            return BadRequest();
        }  
        return this.Ok(region);
    }

    [HttpPost]
    public async Task<IActionResult> AddOne(Region request)
    {
        IActionResult result = this.BadRequest();

        var addedRegion = await _regionService.AddRegion(new Region()
        {
            Name = request.Name
        });

        if(addedRegion == null)
        {
            return BadRequest();
        }  
        return Ok(addedRegion);
    }

    [HttpPut]
    [Route("{id}")]

    public async Task<IActionResult> UpdateRegion(int id, Region request)
    {
        var updatedRegion = await _regionService.UpdateRegion(id, request);

        if (updatedRegion == null)
        {
            BadRequest();
        }

        return Ok(updatedRegion);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteRegion(int id)
    {
        var regionList = await this._regionService.DeleteRegion(id);

        if(regionList == null)
        {
            return BadRequest();
        }  

        return Ok(regionList);
    }
}

