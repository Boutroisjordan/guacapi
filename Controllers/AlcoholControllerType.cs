using Microsoft.AspNetCore.Mvc;
using GuacAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace GuacAPI.Controllers;

using GuacAPI.Services;

[Route("[controller]")]
[ApiController]
public class AlcoholControllerType : ControllerBase
{
    #region Fields

    private IAlcoholService _alcoholService;

    #endregion

    #region Constructor

    public AlcoholControllerType(IAlcoholService alcoholService)
    {
        _alcoholService = alcoholService;
    }

    #endregion

    #region methods

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllTypes()
    {
        var types = await _alcoholService.GetAllTypes();

        if (types == null)
        {
            return BadRequest();
        }
        else if (types.Count == 0)
        {
            return NoContent();
        }

        return Ok(types);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAlcoholTypeById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var type = await _alcoholService.GetAlcoholTypeById(id);
        return Ok(type);
    }

    [HttpGet]
    [Route("GetByLabel/{label}")]
    public async Task<IActionResult> GetAlcoholByLabel(string label)
    {
        if (label == "")
        {
            return BadRequest("Label cannot be empty");
        }

        var typeLabel = await _alcoholService.GetAlcoholByLabel(label);
        return Ok(typeLabel);
    }

    [HttpPost]
    public async Task<IActionResult> AddAlcoholType(AlcoholType type)
    {
        var addAlcohol = await _alcoholService.AddAlcoholType(type);

        if (addAlcohol == null)
        {
            return BadRequest();
        }

        return Ok(addAlcohol);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAlcoholType(int id, AlcoholType type)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var updateAlcohol = await _alcoholService.UpdateAlcoholType(id, type);

        if (updateAlcohol == null)
        {
            return BadRequest();
        }

        return Ok(updateAlcohol);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAlcoholType(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var deletAlcohol = await _alcoholService.DeleteAlcoholType(id);
        return Ok(deletAlcohol);
    }

    #endregion
}