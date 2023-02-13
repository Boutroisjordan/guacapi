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
    public async Task<IActionResult> GetAllFunishers()
    {
        var furnisherList = await _furnisherService.GetAllFurnishers();
        if (furnisherList.Count == 0)
        {
            return NoContent();
        }

        return Ok(furnisherList);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetFurnisherById(int id)
    {
        try
        {
            if (id != 0)
            {
                var furnisher = await _furnisherService.GetFurnisherById(id);

                return Ok(furnisher);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("getProducts/{id}")]
    public async Task<IActionResult> GetFurnisherProducts(int id)
    {
        try
        {
            if (id != 0)
            {
                var furnisher = await _furnisherService.GetProductsOfFurnisher(id);

                return Ok(furnisher);
            }

            return BadRequest();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("ByName/{name}")]
    public async Task<IActionResult> GetFurnisherByName(string name)
    {
        try
        {
            if (name != "")
            {
                var furnisher = await _furnisherService.GetFurnisherByName(name);
                return Ok(furnisher);
            }

            return BadRequest("Name must be not empty");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateFurnisher(FurnisherRegister furnisher)
    {
        try
        {
            var createFurnisher = await _furnisherService.CreateFurnisher(furnisher);
            if (createFurnisher != null)
            {
                return Ok(createFurnisher);
            }

            return BadRequest("Furnisher not created");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateFurnisher(int id, FurnisherRegister furnisher)
    {
        try
        {
            if (id != 0)
            {
                var updateFurnisher = await _furnisherService.UpdateFurnisher(id, furnisher);
                if (updateFurnisher != null)
                {
                    return Ok(updateFurnisher);
                }

                return BadRequest("Furnisher not updated");
            }

            return BadRequest("Id must be not empty");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteFurnisher(int id)
    {
        try
        {
            if (id <= 0) return BadRequest("Id must be greater than 0");
            var deleteFurnisher = await _furnisherService.DeleteFurnisher(id);
            if (deleteFurnisher == null)
            {
                return BadRequest("Furnisher does not exist");
            }

            return Ok(deleteFurnisher);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}