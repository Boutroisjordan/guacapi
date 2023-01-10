using GuacAPI.Models;
using GuacAPI.Context;

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

    // public async Task<List<Product>> GetAllProducts()
    // {
    //     //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
    //     // var query = from Product in this._context.Products
    //         var products = await _context.Products;
    //         return products ;
    // }
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

     public Product AddProduct(Product product)
     {
    //    this._context.Products.Add(product);
    //     _context.SaveChanges();
        return  this._context.Products.Add(product).Entity;
     }

    public void SaveChanges() {
        this._context.SaveChanges();
    }
     public Product? UpdateProduct(int id, Product request)
     {

        var product =  this._context.Products.Find(id);

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