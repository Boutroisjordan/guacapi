using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class OfferController : ControllerBase
{
    #region Fields

    private IOfferService _offerService;
    private readonly IWebHostEnvironment _environment;

    #endregion

    #region Constructors

    public OfferController(IOfferService offerService, IWebHostEnvironment environment)
    {
        this._offerService = offerService;
        this._environment = environment;
    }

    #endregion

    /// <summary>
    /// Récupère toutes les offres
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllOffers()
    {
        var offerList = await _offerService.GetAllOffers();
        if (offerList == null)
        {
            return BadRequest();
        }
        else if (offerList.Count == 0)
        {
            return NoContent();
        }
        return Ok(offerList);
    }

    /// <summary>
    /// Récupère toutes les offres brouillon
    /// </summary>
    [HttpGet("draft")]
    public async Task<IActionResult> GetAllDraftOffers()
    {
        var offerList = await _offerService.GetDraftOffer();
        if (offerList == null)
        {
            return BadRequest();
        }
        else if (offerList.Count == 0)
        {
            return NoContent();
        }
        return Ok(offerList);
    }

    /// <summary>
    /// Récupère une offre
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOneOffer(int id)
    {
        var offer = await _offerService.GetOfferById(id);

        if (offer == null)
        {
            return BadRequest();
        }
        return this.Ok(offer);
    }

    /// <summary>
    /// Vérifie la disponibilité d'une offre
    /// </summary>
    [HttpGet]
    [Route("checkOfferIsAvailable/{id}")]
    public async Task<IActionResult> checkAvailabilityOfOneOffer(int id)
    {
        var offer = await _offerService.checkAvailabilityOfOneOffer(id);

        return this.Ok(offer);
    }

    /// <summary>
    /// Récupère les offres valables (deadline, stock, restock auto)
    /// </summary>
    [HttpGet]
    [Route("availableOffer")]
    public async Task<IActionResult> GetAvailableOffer()
    {
        var offer = await _offerService.GetAvailableOffers();

        //    if (offer == null)
        //    {
        //        return BadRequest();
        //    }
        return this.Ok(offer);
    }

    /// <summary>
    /// Récupère les offres non valables 
    /// </summary>
    [HttpGet]
    [Route("unavailableOffer")]
    public async Task<IActionResult> GetUnavailableOffer()
    {
        var offer = await _offerService.GetUnavailableOffers();

        //    if (offer == null)
        //    {
        //        return BadRequest();
        //    }
        return this.Ok(offer);
    }

    /// <summary>
    /// Ajoute une offre
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddOne(OfferRegister request)
    {

        var addedOffer = await _offerService.AddOffer(request);

        if (addedOffer == null)
        {
            return BadRequest();
        }
        return Ok(addedOffer);
    }

    /// <summary>
    /// Met à jour une offre
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateOffer(int id, OfferRegister request)
    {
        var updatedOffer = await _offerService.UpdateOffer(id, request);

        if (updatedOffer == null)
        {
            BadRequest();
        }

        return Ok(updatedOffer);
    }

    /// <summary>
    /// Supprime une offre
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteOffer(int id)
    {
        var offer = await this._offerService.DeleteOffer(id);

        if (offer == null)
        {
            return BadRequest();
        }

        return Ok(offer);
    }

    /// <summary>
    /// Télécharge une image et renvoi son lien stocker sur le serveur
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("UploadFile")]
    public async Task<String> UploadImage(IFormFile inputFile)
    {
        // bool Results = false;
        try
        {
            var file = Request.Form.Files[0];
            string fName = file.FileName;

            string[] splitpath = file.FileName.Split('.');
            var myUniqueFileName = string.Format(@"{0}." + splitpath[1], DateTime.Now.Ticks);
            string path = Path.Combine("Images", myUniqueFileName);
            string absolutePath = Path.Combine(_environment.ContentRootPath, path);
            if (System.IO.File.Exists(absolutePath))
            {
                throw new Exception("file name already exist");
            }
            using (var stream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            // return $"{file.FileName} successfully uploaded to the Server";
            return path;
        }
        catch (Exception ex)
        {
            throw new Exception($"error : {ex}");
        }
    }

    /// <summary>
    /// Supprime une image
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("DeleteFile")]
    public IActionResult DeleteImage(string path)
    {

        try
        {
            var completePath = Path.Combine(_environment.ContentRootPath, path);
            if (!System.IO.File.Exists(completePath))
            {
                throw new Exception("file doesn't exist");
            }

            System.IO.File.Delete(completePath);
            return Ok("Image Deleted");
        }
        catch (Exception ex)
        {
            throw new Exception($"error : {ex}");
        }
    }
}

