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


 [Authorize (Roles = "Admin")]
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


      [Authorize (Roles = "Admin")]
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

     [HttpPut]
      [Authorize (Roles = "Admin")]
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


      [Authorize (Roles = "Admin")]
     [HttpPut]
     [Route("ChangeStatus/{id}/{statusId}")]
     public async Task<IActionResult> UpdateOrder(int id, int statusId)
     {
         var updatedOrder = await _orderService.UpdateStatus(id, statusId);

         if (updatedOrder == null)
         {
             BadRequest();
         }

         return Ok(updatedOrder);
     }


 [Authorize (Roles = "Admin")]
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
    // [HttpPost("UploadFile")]
    //  public async Task<String> UploadImage(IFormFile inputFile)
    //  {
    //     // bool Results = false;
    //     try {
    //         var file = Request.Form.Files[0];
    //         string fName = file.FileName;
            
    //         string[] splitpath = file.FileName.Split('.');
    //         var myUniqueFileName = string.Format(@"{0}." + splitpath[1], DateTime.Now.Ticks);
    //         string path = Path.Combine("Images", myUniqueFileName);
    //         string absolutePath = Path.Combine(_environment.ContentRootPath, path);
    //         if(System.IO.File.Exists(absolutePath))
    //         {
    //             throw new Exception("file name already exist");
    //         }
    //         using (var stream = new FileStream(absolutePath, FileMode.Create))
    //         {
    //             await file.CopyToAsync(stream);
    //         }
    //         // return $"{file.FileName} successfully uploaded to the Server";
    //         return path;
    //     } 
    //     catch (Exception ex) {
    //         throw new Exception($"error : {ex}");
    //     }
    //  }
    // [HttpGet("DeleteFile")]
    //  public IActionResult DeleteImage(string path)
    //  {
    //     // bool Results = false;
    //     try {
    //         var completePath = Path.Combine(_environment.ContentRootPath, path);
    //         if(!System.IO.File.Exists(completePath))
    //         {
    //             throw new Exception("file doesn't exist");
    //         }

    //         System.IO.File.Delete(completePath);
    //         // return $"{file.FileName} successfully uploaded to the Server";
    //         return Ok("Image Deleted");
    //     } 
    //     catch (Exception ex) {
    //         throw new Exception($"error : {ex}");
    //     }
    //  }
}

