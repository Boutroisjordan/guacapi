using GuacAPI.Models;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    #region Fields

    private ICountryService _countryService;

    #endregion
    
    #region Constructor
    
    public CountryController(ICountryService countryService)
    {
        _countryService = countryService;
    }
    
    #endregion
    
    [HttpGet]
    public async Task<IActionResult> GetAllCountries()
    {
        var countries = await _countryService.GetAllCountries();

        if(countries.Count == 0)
        {
            NoContent();
        }
        return Ok(countries);
    }
    
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var country = await _countryService.GetOne(id);

        if(country == null)
        {
            NotFound();
        }
        return Ok(country);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddOne(Country country)
    {
        var newCountry = await _countryService.AddCountry(new Country()
        {
            Name = country.Name
        });

        return Ok(newCountry);
    }
    
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateOne(int id, Country request)
    {
        var updatedCountry = await _countryService.UpdateCountry(id, new Country()
        {
            Name = request.Name
        });
        
        if (updatedCountry == null)
        {
            NotFound();
        }

        return Ok(updatedCountry);
    }
    
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var deletedCountry = await _countryService.DeleteCountry(id);

        if (deletedCountry == null)
        {
            NotFound();
        }

        return Ok(deletedCountry);
    }
}