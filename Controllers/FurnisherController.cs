using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

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

    [HttpGet]
    [Route("{id}}")]
    public IActionResult GetFurnisherById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var furnisher = this._furnisherService.GetFurnisherById(id);
        return Ok(furnisher);
    }

    [HttpGet]
    [Route("ByName/{name}")]
    public IActionResult GetFurnisherByName(string name)
    {
        if (name == "")
            return BadRequest("Name must be not empty");
        var furnisher = this._furnisherService.GetFurnisherByName(name);
        return Ok(furnisher);
    }

    [HttpPost]

    public IActionResult CreateFurnisher(Furnisher furnisher)
    {
        var createFurnisher = this._furnisherService.CreateFurnisher(furnisher);
        return Created("Furnisher Created", createFurnisher);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateFurnisher(int id, Furnisher furnisher)
    {
        if (id <= 0) return BadRequest("Id must be greater than 0");
        var updateFurnisher = this._furnisherService.UpdateFurnisher(id, furnisher);
        return Ok(updateFurnisher);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteFurnisher(int id)
    {
        if (id <= 0) return BadRequest("Id must be greater than 0");
        var deleteFurnisher = this._furnisherService.DeleteFurnisher(id);
        return Ok(deleteFurnisher);
    }
}