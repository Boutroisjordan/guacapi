using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class DomainController : ControllerBase
{
    #region Fields

    private IDomainService _domainService;

    #endregion

    #region Constructors

    public DomainController(IDomainService domainService)
    {
        this._domainService = domainService;
    }

    #endregion


    [HttpGet]
    public async Task<IActionResult> GetAllDomains()
    {
        var domainList = await _domainService.GetAllDomains();
        if(domainList.Count == 0)
        {
            return NoContent();
        }
        return Ok(domainList);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetDomainById(int id)
    {
        var domain = await _domainService.GetDomainById(id);
        if(domain == null)
        {
            return BadRequest();
        }
        return Ok(domain);
    }

    [HttpGet]
    [Route("GetByName/{name}")]
    public IActionResult GetDomainByName(string name)
    {
        var domain = this._domainService.GetDomainByName(name);
        return Ok(domain);
    }

    [HttpPost]
    public async Task<IActionResult> AddDomain(Domain request)
    {

        var domain = await _domainService.AddDomain(request);

            if(domain is null) 
            {
                return BadRequest();
            }

        return Ok(domain);
    }

    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateDomain(int id, Domain request)
    {

        var domain = await _domainService.UpdateDomain(id, request);

            if(domain is null) 
            {
                return BadRequest();
            }
        return Ok(domain);
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteDomain(int id)
    {
        IActionResult response = BadRequest();

            var domain = await _domainService.DeleteDomain(id);

            if(domain is null) 
            {
                return BadRequest();
            }


        return Ok(domain);
    }
}