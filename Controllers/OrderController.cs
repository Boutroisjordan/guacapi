using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;
using System.IO;
using AutoMapper;
using GuacAPI.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    #region Fields

    private IOrderService _orderService;
    private IOrderOfferService _orderOfferService;
    private readonly IWebHostEnvironment _environment;
    private readonly IMapper _mapper;

    #endregion

    #region Constructors

    public OrderController(IOrderService orderService, IWebHostEnvironment environment, IMapper mapper, IOrderOfferService orderOfferService)
    {
        this._orderService = orderService;
        this._environment = environment;
        this._mapper = mapper;
        this._orderOfferService = orderOfferService;
    }

    #endregion

    /// <summary>
    /// Récupère toutes les commande utilisateurs
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orderList = await _orderService.GetAllOrders();
        if (orderList == null)
        {
            return BadRequest();
        }
        else if (orderList.Count == 0)
        {
            return NoContent();
        }
        return Ok(orderList);
    }

    /// <summary>
    /// Récupère tous les status de commandes
    /// </summary>
    [HttpGet("statusOrder")]
    public async Task<IActionResult> GetAllStatus()
    {
        var orderList = await _orderService.GetAllStatus();
        if (orderList == null)
        {
            return BadRequest();
        }
        else if (orderList.Count == 0)
        {
            return NoContent();
        }
        return Ok(orderList);
    }

    /// <summary>
    /// Récupère une commande
    /// </summary>
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var order = await _orderService.GetOne(id);

        if (order == null)
        {
            return BadRequest();
        }
        return this.Ok(order);
    }

    /// <summary>
    /// Route pour l'acion "commander", change le status et le stock produit
    /// </summary>
    [HttpGet]
    [Route("commander/{id}")]
    public async Task<IActionResult> Commander(int id)
    {
        var order = await _orderService.Commander(id);

        if (order == null)
        {
            return BadRequest();
        }
        return this.Ok(order);
    }

    /// <summary>
    /// Créer une commande
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddOne(OrderRegistryDTO request)
    {

        var addedOffer = await _orderService.Add(request);

        if (addedOffer == null)
        {
            return BadRequest();
        }
        return Ok(addedOffer);
    }

    /// <summary>
    /// Ajoute une offre à une commande (deprecated)
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPost("orderOffer")]
    public async Task<IActionResult> AddOrderOffer(OrderOfferRegistryDTO request)
    {

        var addedOffer = await _orderOfferService.Add(request);

        if (addedOffer == null)
        {
            return BadRequest();
        }
        return Ok(addedOffer);
    }

    /// <summary>
    /// Met à jour une commande
    /// </summary>
    [HttpPut]
    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    public async Task<IActionResult> UpdateOrder(int id, OrderUpdateDTO request)
    {
        var updatedOffer = await _orderService.Update(id, request);

        if (updatedOffer == null)
        {
            BadRequest();
        }

        return Ok(updatedOffer);
    }

    /// <summary>
    /// Met à jour le status une commande
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpPut]
    [Route("ChangeStatus/{id}/{statusId}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, int statusId)
    {
        var updatedOrder = await _orderService.UpdateStatus(id, statusId);

        if (updatedOrder == null)
        {
            BadRequest();
        }
        return Ok(updatedOrder);
    }

    /// <summary>
    /// Supprime une commande
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await this._orderService.Delete(id);

        if (order == null)
        {
            return BadRequest();
        }

        return Ok(order);
    }

}

