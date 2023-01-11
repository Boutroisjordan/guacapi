using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

namespace GuacAPI.Controllers;

[Route("api/[controller]")]
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


    [HttpGet("GetAllFunisher")]
    public IActionResult GetAllFunishers()
    {
        var furnisherList = this._furnisherService.GetAllFurnishers();
        return Ok(furnisherList);
    }

    [HttpGet("{id}")]
    public IActionResult GetFurnisherById(int id)
    {
        var furnisher = this._furnisherService.GetFurnisherById(id);
        return Ok(furnisher);
    }

    [HttpGet("{name}")]
    public IActionResult GetFurnisherByName(string name)
    {
        var furnisher = this._furnisherService.GetFurnisherByName(name);
        return Ok(furnisher);
    }

    [HttpPost("addFurnisher")]
    public IActionResult CreateFurnisher(Furnisher furnisher)
    {
        var createFurnisher = this._furnisherService.CreateFurnisher(furnisher);
        return Created("Furnisher Created", createFurnisher);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateFurnisher(int id, Furnisher furnisher)
    {
        var updateFurnisher = this._furnisherService.UpdateFurnisher(id, furnisher);
        return Ok(updateFurnisher);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteFurnisher(int id)
    {
        var deleteFurnisher = this._furnisherService.DeleteFurnisher(id);
        return Ok(deleteFurnisher);
    }
}