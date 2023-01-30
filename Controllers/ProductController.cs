using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Services;

namespace GuacAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    #region Fields
    private IProductService _productService;
    #endregion

    #region Constructors
    public ProductController(IProductService productService)
    {
        this._productService = productService;
    }
    #endregion


    [HttpGet]
     public async Task<IActionResult> GetAllProducts()
    {
        var productList = await _productService.GetAllProducts();
        if (productList == null) 
        {
            return BadRequest();
        } else if (productList.Count == 0) {
            return NoContent();
        }
         return Ok(productList);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetOneProduct(int id)
    {
        var product = await _productService.GetOne(id);

        if(product == null) {
            return BadRequest();
        }
         return Ok(product);
    }

    [HttpGet]
    [Route("stock/{id}")]
        public async Task<IActionResult> GetStock(int id)
        {
            var productStock = await _productService.CheckStock(id);

            return Ok(productStock);
        }
    [HttpPost]
  public async Task<IActionResult> AddOne(Product request)
    {

        var addedProduct = await _productService.AddProduct(request);

        if (addedProduct == null) 
        {
            return BadRequest();
        }

        return Ok(addedProduct);
    }

    [HttpPut]
    [Route("{id}")]

    public async Task<IActionResult> UpdateProduct(int id, Product request)
    {
        var updatedProduct = await _productService.UpdateProduct(id, request);

        if(updatedProduct == null) {
            return BadRequest();
        }

        return Ok(updatedProduct);
    }

    [HttpDelete]
     [Route("{id}")]
     public async Task<IActionResult> DeleteProduct(int id)
     {
         var productList =  await this._productService.DeleteProduct(id);

        if(productList == null) 
        {
            return BadRequest();
        }

         return Ok(productList);
     }
}

