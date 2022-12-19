using Microsoft.EntityFrameworkCore;
using GuacAPI.Context.TypeConfigurations;
using GuacAPI.Models;
using GuacAPI.Interface;


namespace GuacAPI.Context;

public class ProductContext : DbContext, IUnitOfWork
{
    #region Constructor
     public ProductContext(DbContextOptions<ProductContext> options) : base(options)
     {

     }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
    }

    public  IUnitOfWork SaveChnages()
    {
        throw new NotImplementedException();
    }

    public DbSet<Product> Products {get; set;} = null!;
    public DbSet<Furnisher> Furnishers {get; set;} = null!;
}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video