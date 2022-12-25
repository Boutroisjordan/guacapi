using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.HasKey(item => item.ProductId);

         builder.HasOne(item => item.furnisher)
             .WithMany(item => item.Products);

         builder.HasOne(item => item.domain)
         .WithMany(item => item.Products);

         builder.HasOne(item => item.region)
         .WithMany(item => item.Products);

         builder.HasOne(item => item.millesime)
         .WithMany(item => item.Products);

         builder.HasOne(item => item.alcoholType)
         .WithMany(item => item.Products);

         builder.HasOne(item => item.appellation)
         .WithMany(item => item.Products);

    }
    #endregion

}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video