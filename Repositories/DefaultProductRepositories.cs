using GuacAPI.Interface;
using GuacAPI.Context;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Repositories;

public class DefaultProductRepository : IProductRepository
{
    #region Fields
        private readonly ProductContext _context;
    #endregion

    #region Constructors
    public DefaultProductRepository(ProductContext context) 
    {
        this._context = context;
    }

    #endregion

    #region Public methods
    public ICollection<Product> GetAll()
    {
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        // var query = from Product in this._context.Products
        // select Product;s
        return this._context.Products.ToList();
    }
    public ICollection<Product> GetOne(int id)
    {
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        // var query = from Product in this._context.Products
        // select Product;s
        return this._context.Products.ToList();
    }

    public Product AddOne(Product item)
    {
        return this._context.Products.Add(item).Entity;
    }


    #endregion

    #region Properties
    public IUnitOfWork UnitOfWork => this._context;

    #endregion
}