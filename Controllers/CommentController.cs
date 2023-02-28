using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore;

namespace GuacAPI.Controllers;
 
[Route("[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    #region Fields

    private ICommentService _commentService;

    #endregion

    #region Constructors

    public CommentController(ICommentService commentService)
    {
        this._commentService = commentService;
    }

    #endregion

    // [HttpGet,AllowAnonymous]
    [HttpGet]
    // [SwaggerOperation(Description = "Obtient une liste de tous les articles")]
    public async Task<IActionResult> GetAll()
    {
        var productList = await _commentService.GetAll();
        if (productList == null)
        {
            return BadRequest();
        }
        else if (productList.Count == 0)
        {
            return NoContent();
        }

        return Ok(productList);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var product = await _commentService.GetCommentById(id);

        if (product == null)
        {
            return BadRequest();
        }

        return Ok(product);
    }

 [Authorize (Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> AddOne(CommentRegister request)
    {
        var addedProduct = await _commentService.AddComment(request);

        if (addedProduct == null)
        {
            return BadRequest();
        }

        return Ok(addedProduct);
    }

 [Authorize (Roles = "Admin")]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, CommentRegister request)
    {
        var updatedProduct = await _commentService.UpdateComment(id, request);

        if (updatedProduct == null)
        {
            return BadRequest();
        }

        return Ok(updatedProduct);
    }


    [HttpDelete, Authorize (Roles = "Admin")]
    [Route("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var productList = await this._commentService.DeleteComment(id);

        if (productList == null)
        {
            return BadRequest();
        }

        return Ok(productList);
    }

 [Authorize (Roles = "Admin")]
    [HttpDelete]
    [Route("{id}/{userId}")]
    public async Task<IActionResult> OwnerDeleteComment(int id, int userId)
    {
        var productList = await this._commentService.OwnerDeleteComment(id, userId);

        if (productList == null)
        {
            return BadRequest();
        }

        return Ok(productList);
    }
}