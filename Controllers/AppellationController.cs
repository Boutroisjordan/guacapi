using GuacAPI.Models;
using GuacAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuacAPI.Controllers;

[Route("[controller]")]
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

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetAppellations()
    {
        var result = _appellationService.GetAppellations();
        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetAppellationById(int id)
    {
        var result = _appellationService.GetAppellationById(id);
        return Ok(result);
    }

    [HttpGet]
    [Route("ByName/{id}")]
    public IActionResult GetAppellationByName(string name)
    {
        var result = _appellationService.GetAppellationByName(name);
        return Ok(result);
    }

    [HttpPost]
    [Route("{id}")]
    public IActionResult CreateAppellation(Appellation appellation)
    {
        var result = _appellationService.CreateAppellation(appellation);
        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateAppellation(int id, Appellation appellation)
    {
        var result = _appellationService.UpdateAppellation(id, appellation);
        return Ok(result);
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteAppellation(int id)
    {
        var result = _appellationService.DeleteAppellation(id);
        return Ok(result);
    }

    #endregion
}