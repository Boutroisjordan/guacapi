﻿using GuacAPI.Models;
using GuacAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Récupère toute les appellations
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAppellations()
    {
        var result = await _appellationService.GetAppellations();

        if (result == null)
        {
            return BadRequest();
        }
        else if (result.Count == 0)
        {
            return NoContent();
        }
        return Ok(result);
    }

    /// <summary>
    /// Récupère une appellation
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetAppellationById(int id)
    {
        var result = await _appellationService.GetAppellationById(id);

        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    /// <summary>
    /// Récupère une appellation par son nom
    /// </summary>
    [HttpGet]
    [Route("GetByName/{name}")]
    public async Task<IActionResult> GetAppellationByName(string name)
    {
        var result = await _appellationService.GetAppellationByName(name);

        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    /// <summary>
    /// Créer une appellation 
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateAppellation(Appellation appellation)
    {
        var result = await _appellationService.CreateAppellation(appellation);

        if (result == null)
        {
            return BadRequest("probleme");
        }

        return Ok(result);
    }

    /// <summary>
    /// Met à jour une appellation 
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateAppellation(int id, Appellation appellation)
    {
        var result = await _appellationService.UpdateAppellation(id, appellation);

        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    /// <summary>
    /// Supprime une appellation 
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteAppellation(int id)
    {
        var result = await _appellationService.DeleteAppellation(id);

        if (result == null)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    #endregion
}