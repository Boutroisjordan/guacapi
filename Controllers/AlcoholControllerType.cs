using Microsoft.AspNetCore.Mvc;
using GuacAPI.Models;

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
    public ActionResult<AlcoholType> GetAllTypes()
    {
        var types = _alcoholService.GetAllTypes();

        return Ok(types);
    }

    [HttpGet]
    [Route("{id}")]
    public ActionResult<AlcoholType> GetAlcoholTypeById(int id)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var type = _alcoholService.GetAlcoholTypeById(id);
        return Ok(type);
    }

    [HttpGet]
    [Route("ByLabel/{label}")]
    public ActionResult<AlcoholType> GetAlcoholByLabel(string label)
    {
        if (label == "")
        {
            return BadRequest("Label cannot be empty");
        }

        var typeLabel = _alcoholService.GetAlcoholByLabel(label);
        return Ok(typeLabel);
    }

    [HttpPost]
    [Route("{id}")]
    public ActionResult<AlcoholType> AddAlcoholType(AlcoholType type)
    {
        var addAlcohol = _alcoholService.AddAlcoholType(type);
        return Created($"/api/alcohol/{type.AlcoholTypeId}", addAlcohol);
    }

    [HttpPut]
    [Route("{id}")]
    public ActionResult<AlcoholType> UpdateAlcoholType(int id, AlcoholType type)
    {
        if (id <= 0)
        {
            return BadRequest("Id must be greater than 0");
        }

        var updateAlcohol = _alcoholService.UpdateAlcoholType(id, type);
        return Ok(updateAlcohol);
    }

    [HttpDelete]
    [Route("{id}")]
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