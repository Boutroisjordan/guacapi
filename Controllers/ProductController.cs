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
    public IActionResult GetAllProducts()
    {
        var productList = this._productService.GetAllProducts();
        return Ok(productList);
    }

    //  public ActionResult<Product> GetOne(int id)
    //  {
    //      var product = _productService.GetOne(id);
    //      return Ok(product);
    //  }


    [HttpGet]
    [Route("{id}")]
    public IActionResult GetOneProduct(int id)
    {

        var product = _productService.GetOne(id);
        // var model = productsList.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId});
        // var model = productList.Select(item => new Product() { 
        //     Name = item.Name, 
        //     Price = item.Price, 
        //     alcoholType = item.alcoholType, 
        //     Reference = item.Reference, 
        //     Millesime = item.Millesime, 
        //     FurnisherId = item.FurnisherId,
        //     AppellationId = item.AppellationId,
        //     DomainId = item.DomainId,
        //     RegionId = item.RegionId,
        //     AlcoholDegree = item.AlcoholDegree,
        //     Stock = item.Stock
        // }
        // );
        return this.Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> AddOne(Product request)
    {
        IActionResult result = this.BadRequest();

        var addedProduct = await _productService.AddProduct(new Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock,
            Millesime = request.Millesime,
            AlcoholDegree = request.AlcoholDegree,
            AlcoholTypeId = request.AlcoholTypeId,
            Reference = request.Reference,
            FurnisherId = request.FurnisherId,
            DomainId = request.DomainId,
            RegionId = request.RegionId,
            AppellationId = request.AppellationId,


        });


        // if (addedProduct != null)
        // {
        //     request.ProductId = addedProduct.ProductId;
        //     result = this.Ok(result);
        // }

        // // this._repository.UnitOfWork.SaveChanges();
        // this._productService.SaveChanges();

        return this.Ok(addedProduct);
    }

    [HttpPut]
    [Route("{id}")]

    public async Task<IActionResult> UpdateProduct(int id, Product request)
    {
        var updatedProduct = await _productService.UpdateProduct(id, request);

        if(updatedProduct == null) {
            BadRequest();
        }

        this._productService.SaveChanges();
        return Ok(updatedProduct);
    }

     [HttpDelete]
     [Route("{id}")]
     public async Task<IActionResult> DeleteProduct(int id)
     {
         var productList =  await this._productService.DeleteProduct(id);

         return Ok(productList);
     }
}

