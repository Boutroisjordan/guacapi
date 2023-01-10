using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;
using GuacAPI.Repositories;
using GuacAPI.Interface;
using GuacAPI.DTOs;
namespace GuacAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    #region Fields
        private IProductRepository _repository;
    #endregion

    #region Constructors
    public ProductsController(IProductRepository repository)
    {
        this._repository = repository;
    }
    #endregion

    #region Public methods
    [HttpGet]
    public IActionResult GetAll()
    {
        // var model = Enumerable.Range(1,10).Select(item => new Product() {ProductId = item});
        // return this.Ok(model);

        var productsList = this._repository.GetAll();

        // var model = productsList.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId});
        var model = productsList.Select(item => new ProductDto() { Name = item.Name, Price = item.Price, Category = item.Category, Reference = item.Reference, FurnisherId = item.FurnisherId});
        return this.Ok(model);
    }
    public IActionResult GetOne([FromQuery] int furnisherId)
    {
        // var model = Enumerable.Range(1,10).Select(item => new Product() {ProductId = item});
        // return this.Ok(model);

        var productsList = this._repository.GetAll();

        // var model = productsList.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId});
        var model = productsList.Select(item => new ProductDto() { Name = item.Name, Price = item.Price, Category = item.Category, Reference = item.Reference, FurnisherId = item.FurnisherId});
        return this.Ok(model);
    }

    [HttpPost]
    public IActionResult AddOne(ProductDto dto)
    {
         IActionResult result = this.BadRequest();

         Product addProduct = this._repository.AddOne(new Product() 
         {
             Name = dto.Name,
             Category = dto.Category,
             Price = dto.Price,
             FurnisherId = dto.FurnisherId
            //  DomainId = dto.DomainId
         });

         if (addProduct != null)
         {
            dto.Id = addProduct.ProductId;
             result = this.Ok(dto);
         }

        this._repository.UnitOfWork.SaveChanges();

        return this.Ok(result);
    }
    #endregion

    // [Route("api/[controller]/{id}")]
    //GET: api/Products
   /* [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        if (_dbContext.Products == null)
        {
            return NotFound();
        }
        return await _dbContext.Products.ToListAsync();
    }*/

    //GET: api/Products/5
   /* [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        if (_repository == null)
        {
            return NotFound();
        }
        var product = await _repository.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }
        return product;
    } */

} 