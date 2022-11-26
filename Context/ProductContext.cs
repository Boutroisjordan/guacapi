using Microsoft.EntityFrameworkCore;
using GuacAPI.Context.TypeConfigurations;
using GuacAPI.Models;


namespace GuacAPI.Context;

public class ProductContext : DbContext
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
    public DbSet<Product> Products {get; set;} = null!;
    public DbSet<Furnisher> Furnishers {get; set;} = null!;
}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video