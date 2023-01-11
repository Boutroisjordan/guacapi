using Microsoft.AspNetCore.Mvc;
using GuacAPI.Models;

namespace GuacAPI.Controllers;

using GuacAPI.Services;

[Route("api/[controller]")]
[ApiController]
public class AlcoholController : ControllerBase
{
    #region fields

    private IAlcoholService _alcoholService;

    #endregion

    #region constructor

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
        var type = _alcoholService.GetAlcoholTypeById(id);
        return Ok(type);
    }

    [HttpGet("{label}")]
    public ActionResult<AlcoholType> GetAlcoholByLabel(string label)
    {
        var typeLabel = _alcoholService.GetAlcoholByLabel(label);
        return Ok(typeLabel);
    }

    [HttpPost("addAlcoholType")]
    public ActionResult<AlcoholType> AddAlcoholType(AlcoholType type)
    {
        var addAlcohol = _alcoholService.AddAlcoholType(type);
        return Ok(addAlcohol);
    }

    [HttpPut("{id}")]
    public ActionResult<AlcoholType> UpdateAlcoholType(int id, AlcoholType type)
    {
        var updateAlcohol = _alcoholService.UpdateAlcoholType(id, type);
        return Ok(updateAlcohol);
    }

    [HttpDelete("{id}")]
    public ActionResult<AlcoholType> DeleteAlcoholType(int id)
    {
        var deletAlcohol = _alcoholService.DeleteAlcoholType(id);
        return Ok(deletAlcohol);
    }

    #endregion
}