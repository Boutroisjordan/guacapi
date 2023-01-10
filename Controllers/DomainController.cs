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
        public IActionResult GetAllFunishers()
        {
            var domainList = this._domainService.GetAllDomains();
            return Ok(domainList);
        }
}

