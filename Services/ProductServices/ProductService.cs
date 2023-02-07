using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace GuacAPI.Services;
 
public class ProductService : IProductService
{
    #region Fields
    private readonly DataContext _context;

        // private readonly IOfferService _offerService;
    #endregion

    // #region Constructors
    public ProductService(DataContext context)
    {
        this._context = context;
        // this._offerService = offerService;
    }

    public async Task<List<Product>> GetAllProducts()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }


    public async Task<Product> GetOne(int id)
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

    public async Task<Product> UpdateProduct(int id, Product request)
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


            await _context.SaveChangesAsync();

            return product;
        }

        return null;
    }

    public async Task<List<Product>> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
            return null;

        _context.Products.Remove(product);

        await _context.SaveChangesAsync();

        return _context.Products.ToList();
    }

    public async Task<int> CheckStock(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if(product is null) {
            throw new Exception("Offer not found");
        }
        return product.Stock;
    }
}