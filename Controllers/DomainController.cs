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
    public IActionResult GetAllDomains()
    {
        var domainList = this._domainService.GetAllDomains();
        return Ok(domainList);
    }

    [HttpGet("[controller]/{id}")]
    public IActionResult GetDomainById(int id)
    {
        var domain = this._domainService.GetDomainById(id);
        return Ok(domain);
    }

    [HttpGet("[controller]/{name}")]
    public IActionResult GetDomainByName(string name)
    {
        var domain = this._domainService.GetDomainByName(name);
        return Ok(domain);
    }

    [HttpPost("[controller]/domain")]
    public IActionResult AddDomain(Domain request)
    {
        IActionResult response = BadRequest();

        if (ModelState.IsValid)
        {
            var domain = this._domainService.AddDomain(request);
            response = Ok(domain);
        }

        this._domainService.SaveChanges();
        return response;
    }

    [HttpPut("[controller]/{id}")]
    public IActionResult UpdateDomain(int id, Domain request)
    {
        IActionResult response = BadRequest();

        if (ModelState.IsValid)
        {
            var domain = this._domainService.UpdateDomain(id, request);
            response = Ok(domain);
        }

        this._domainService.SaveChanges();
        return response;
    }

    [HttpDelete("[controller]/{id}")]
    public IActionResult DeleteDomain(int id)
    {
        IActionResult response = BadRequest();

        if (ModelState.IsValid)
        {
            var domain = this._domainService.DeleteDomain(id);
            response = Ok(domain);
        }

        this._domainService.SaveChanges();
        return response;
    }
}