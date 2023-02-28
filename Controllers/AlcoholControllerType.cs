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
    private readonly IConfiguration _configuration;

    #endregion

    #region Constructor

    public AlcoholControllerType(IAlcoholService alcoholService, IConfiguration configuration)
    {
        _alcoholService = alcoholService;
        _configuration = configuration;
    }

    #endregion

    #region methods

    /// <summary>
    /// Récupère tous les types d'alcool
    /// </summary>
    [HttpGet]
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

    /// <summary>
    /// Récupère tous les types d'alcool
    /// </summary>
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

    /// <summary>
    /// Récupère un type d'alcool
    /// </summary>
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

    /// <summary>
    /// Créer un type d'alcool
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddAlcoholType(AlcoholTypeRegister type)
    {
        var addAlcohol = await _alcoholService.AddAlcoholType(type);

        if (addAlcohol == null)
        {
            return BadRequest();
        }

        return Ok(addAlcohol);
    }

    /// <summary>
    /// Met à jour un type d'alcool
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAlcoholType(int id, AlcoholTypeRegister type)
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

    /// <summary>
    /// Supprime un type d'alcool
    /// </summary>
    [Authorize(Roles = "Admin")]
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