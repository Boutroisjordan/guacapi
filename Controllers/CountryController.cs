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
    public async Task<IActionResult> Get()
    {
        var countries = await _countryService.GetAllCountries();

        if(countries.Count == 0)
        {
            return NoContent();
        }
        return Ok(countries);
    }


}