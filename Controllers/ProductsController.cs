using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.AspNetCore.Mvc;

namespace GuacAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProductsController : ControllerBase
{
    #region Fields
        private ProductContext _context;
    #endregion

    #region Constructors
    public ProductsController(ProductContext context)
    {
        this._context = context;
    }
    
    #endregion

    #region Public methods
    [HttpGet]
    public IActionResult Get()
    {
        // var model = Enumerable.Range(1,10).Select(item => new Product() {ProductId = item});
        // return this.Ok(model);

        var model = this._context.Products.ToList();
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        

        // var query = from Product in this._context.Products
        // select Product;s

        return this.Ok(model);
        // return this.Ok(query.ToList());
    }
    #endregion

    //GET: api/Products
    /*[HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        if (_dbContext.Products == null)
        {
            return NotFound();
        }
        return await _dbContext.Products.ToListAsync();
    }

    //GET: api/Products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        if (_dbContext == null)
        {
            return NotFound();
        }
        var product = await _dbContext.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }
        return product;
    } */

} 