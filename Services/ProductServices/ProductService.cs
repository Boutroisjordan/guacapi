using GuacAPI.Models;
using GuacAPI.Context;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using GuacAPI.Helpers;
namespace GuacAPI.Services;
 
public class ProductService : IProductService
{
    #region Fields
    private readonly DataContext _context;
    private readonly IMapper _mapper;

        // private readonly IOfferService _offerService;
    #endregion

    // #region Constructors
    public ProductService(DataContext context, IMapper mapper)
    {
        this._context = context;
        this._mapper = mapper;
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

    public async Task<Product> GetByName(string name)
    {
         var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name);
         if(product is null) {
            throw new Exception("Product not found");
         }
        return product;
    }

    public async Task<Product> AddProduct(ProductRegister request)
    {

        // var furnisherById = await _context.Furnishers.FindAsync(request.FurnisherId);

    // var checkDomain = _context.Domains.Where(x => x.Name == request.DomainName).FirstOrDefault();
    // var newDomain
    // if(checkDomain is null) {
    //    newDomain = _context.Domains.Add(request.DomainName)
    // }

        Product product = _mapper.Map<Product>(request);

        Product saveProduct = _context.Products.Add(product).Entity;
        await _context.SaveChangesAsync();
        
        List<ProductOffer> ListProductOffer = new List<ProductOffer>() {
            new ProductOffer() {
                ProductId = saveProduct.ProductId,
                QuantityProduct = 1
            }
        };


        Offer offerunit = new Offer() {
            Name = saveProduct.Name,
            isB2B = false,
            isDraft = false,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            ProductOffers = ListProductOffer
        };

        
        _context.Offers.Add(offerunit);
        await _context.SaveChangesAsync();

        return saveProduct;
    }

    public async Task<Product> UpdateProduct(int id, ProductRegister request)
    {

        var product = await _context.Products.FindAsync(id);

        var newProduct = _mapper.Map(request, product);

        // if (product != null)
        // {

            // product.Name = request.Name;
            // product.Price = request.Price;
            // product.Stock = request.Stock;
            // product.Millesime = request.Millesime;
            // product.AlcoholDegree = request.AlcoholDegree;
            // product.AlcoholTypeId = request.AlcoholTypeId;
            // product.Reference = request.Reference;
            // product.FurnisherId = request.FurnisherId;
            // product.DomainId = request.DomainId;
            // product.RegionId = request.RegionId;
            // product.AppellationId = request.AppellationId;


            await _context.SaveChangesAsync();
            return product;
        // }
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