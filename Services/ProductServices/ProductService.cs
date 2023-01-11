using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;

public class ProductService : IProductService
{
    #region Fields
<<<<<<< HEAD
    private readonly DataContext _context;
=======

    private readonly DataContext _context;

>>>>>>> origin/featuresAddedCrud
    #endregion

    // #region Constructors
    public ProductService(DataContext context)
    {
        this._context = context;
    }

    public async Task<List<Product>?> GetAllProducts()
    {
<<<<<<< HEAD
        var products = await _context.Products.ToListAsync();
        return products;
    }
    public async Task<Product?> GetOne(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product;
    }

    public async Task<Product> AddProduct(Product request)
    {

        var furnisherById = await _context.Furnishers.FindAsync(request.FurnisherId);

        Product addedProduct = new Product()
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
            furnisher = furnisherById
        };


        var saveProduct = _context.Products.Add(addedProduct).Entity;
        await _context.SaveChangesAsync();

        return saveProduct;
    }

=======
        //var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();
        // var query = from Product in this._context.Products
        var products = _context.Products.ToList();
        return products;
    }

    public Product? GetOne(int id)
    {
        // var model = this._context.Products.Select(item => new { Name = item.Name, Price = item.Price, Furnisher = item.FurnisherId}).ToList();

        var product = this._context.Products.Find(id);
        return product;
        //  if(product == null)
        //      return null;
        //  return product;
    }
    
    public async Task<Product?> GetProductByName(string name)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        return product;
    }

    public Product AddProduct(Product product)
    {
        //    this._context.Products.Add(product);
        //     _context.SaveChanges();
        return this._context.Products.Add(product).Entity;
    }

>>>>>>> origin/featuresAddedCrud
    public void SaveChanges()
    {
        this._context.SaveChanges();
    }

<<<<<<< HEAD
    public async Task<Product?> UpdateProduct(int id, Product request)
    {

        var product = await _context.Products.FindAsync(id);

        if (product != null)
        {

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

            var furnisher = await _context.Furnishers.FindAsync(request.FurnisherId);

            product.furnisher = furnisher;

            await _context.SaveChangesAsync();

            return product;
        }

        return null;
    }

    public async Task<List<Product>?> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return null;

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return _context.Products.ToList();
    }

=======
    public Product? UpdateProduct(int id, Product request)
    {
        var product = this._context.Products.Find(id);

        if (product == null)
            return null;

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
>>>>>>> origin/featuresAddedCrud
}