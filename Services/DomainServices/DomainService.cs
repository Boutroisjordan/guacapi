using GuacAPI.Models;
using GuacAPI.Context;

namespace GuacAPI.Services;

public class DomainService : IDomainService
{
    #region Fields
        private readonly DataContext _context;
    #endregion

    // #region Constructors
    public DomainService(DataContext context) 
    {
        this._context = context;
    }

    // public async Task<List<Product>> GetAllProducts()
    // {
    //     //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
    //     // var query = from Product in this._context.Products
    //         var products = await _context.Products;
    //         return products ;
    // }
    public ICollection<Domain> GetAllDomains()
    {
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        // var query = from Product in this._context.Products
            var domains =  _context.Domains.ToList();
            return domains;
    }
     /*public Product? GetOne(int id)
     {
        // var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();

              var product =  this._context.Products.Find(id);
              return product;
            //  if(product == null)
            //      return null;
            //  return product;

     }
     */
    // public Product AddOne()
    // {
    //     //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
    //     // var query = from Product in this._context.Products
    //         var products =  _context.Products.ToList();
    //         return products;
    // }

        // public List<Product> DeleteBook(int id)
        // {
        //     var product = products.Find(x => x.Id == id);
        //     if (product is null)
        //         return null;

        //     productss.Remove(product);

        //     return products;
        // }

        // public List<Product> GetAllProducts()
        // {
        //     return products;
        // }

        // public Product? GetProduct(int id)
        // {
        //     var product = products.Find(x => x.ProductId == id);
        //     if (product is null)
        //         return null;

        //     return product;
        // }

        // public List<Product> UpdateBook(int id, Product request)
        // {
        //     var product = products.Find(x => x.Id == id);
        //     if (product is null)
        //         return null;

        //     product.Author = request.Author;
        //     product.Title = request.Title;
        //     product.Category = request.Category;
        //     product.Year = request.Year;

        //     return products;
        // }
}