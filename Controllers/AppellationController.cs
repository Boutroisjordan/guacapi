using GuacAPI.Models;
using GuacAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuacAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppellationController : ControllerBase
{
    #region Fields

    private readonly IAppellationService _appellationService;

    #endregion

    #region Constructor

    public AppellationController(IAppellationService appellationService)
    {
        _appellationService = appellationService;
    }

    #endregion

    #region Methods

    [HttpGet("GetAllAppellations")]
    public IActionResult GetAppellations()
    {
        var result = _appellationService.GetAppellations();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetAppellationById(int id)
    {
        var result = _appellationService.GetAppellationById(id);
        return Ok(result);
    }

    [HttpGet("{name}")]
    public IActionResult GetAppellationByName(string name)
    {
        var result = _appellationService.GetAppellationByName(name);
        return Ok(result);
    }

    [HttpPost("AddAppellation")]
    public IActionResult CreateAppellation(Appellation appellation)
    {
        var result = _appellationService.CreateAppellation(appellation);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAppellation(int id, Appellation appellation)
    {
        var result = _appellationService.UpdateAppellation(id, appellation);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAppellation(int id)
    {
        var result = _appellationService.DeleteAppellation(id);
        return Ok(result);
    }

    #endregion
}