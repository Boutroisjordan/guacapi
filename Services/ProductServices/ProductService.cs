using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;

public class ProductService : IProductService
{
    #region Fields
        private readonly DataContext _context;
    #endregion

    // #region Constructors
    public ProductService(DataContext context) 
    {
        this._context = context;
    }

    public ICollection<Product> GetAllProducts()
    {
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        // var query = from Product in this._context.Products
            var products =  _context.Products.ToList();
            return products;
    }
     public Product? GetOne(int id)
     {
        // var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();

              var product =  this._context.Products.Find(id);
              return product;
            //  if(product == null)
            //      return null;
            //  return product;

     }

     public async Task<List<Product>> AddProduct(Product product)
     {
    //    this._context.Products.Add(product);
    //     _context.SaveChanges();
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return await _context.Products.ToListAsync();
     }

    public void SaveChanges() {
        this._context.SaveChanges();
    }

     public async Task<Product?> UpdateProduct(int id, Product request)
     {

        var product = await _context.Products.FindAsync(id);

        if(product != null) {

        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        product.Millesime = request.Millesime;
        product.AlcoholDegree = request.AlcoholDegree;
        product.AlcoholTypeId = request.AlcoholTypeId;
        product.Reference = request.Reference;
        product.FurnisherId = request.FurnisherId;
        product.DomainId = request.DomainId;
        product.RegionId = request.RegionId;
        product.AppellationId = request.AppellationId;

        product.ProductId = id;

        return product;
        }

        return null;
     }

         public async Task<List<Product>?> DeleteProduct(int id)
         {
             var product =  await _context.Products.FindAsync(id);
             if (product is null)
                 return null;

             _context.Products.Remove(product);

             await _context.SaveChangesAsync();

             return _context.Products.ToList();
         }
    
}