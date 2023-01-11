using Microsoft.AspNetCore.Mvc;
using GuacAPI.Models;

namespace GuacAPI.Controllers;

using GuacAPI.Services;

[Route("api/[controller]")]
[ApiController]
public class AlcoholController : ControllerBase
{
    #region Fields

    private IAlcoholService _alcoholService;

    #endregion

    #region Constructor

    public AlcoholController(IAlcoholService alcoholService)
    {
        _alcoholService = alcoholService;
    }

    #endregion

    #region methods

    [HttpGet("GetAllAlcoholTypes")]
    public ActionResult<AlcoholType> GetAllTypes()
    {
        var types = _alcoholService.GetAllTypes();

        return Ok(types);
    }

    [HttpGet("{id}")]
    public ActionResult<AlcoholType> GetAlcoholTypeById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var type = _alcoholService.GetAlcoholTypeById(id);
        return Ok(type);
    }

    [HttpGet("{label}")]
    public ActionResult<AlcoholType> GetAlcoholByLabel(string label)
    {
        if (label == "")
        {
            return BadRequest("Label cannot be empty");
        }

        var typeLabel = _alcoholService.GetAlcoholByLabel(label);
        return Ok(typeLabel);
    }

    [HttpPost("addAlcoholType")]
    public ActionResult<AlcoholType> AddAlcoholType(AlcoholType type)
    {
        var addAlcohol = _alcoholService.AddAlcoholType(type);
        return Created($"/api/alcohol/{type.AlcoholTypeId}", addAlcohol);
    }

    [HttpPut("{id}")]
    public ActionResult<AlcoholType> UpdateAlcoholType(int id, AlcoholType type)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var updateAlcohol = _alcoholService.UpdateAlcoholType(id, type);
        return Ok(updateAlcohol);
    }

    [HttpDelete("{id}")]
    public ActionResult<AlcoholType> DeleteAlcoholType(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var deletAlcohol = _alcoholService.DeleteAlcoholType(id);
        return Ok(deletAlcohol);
    }

    #endregion
}