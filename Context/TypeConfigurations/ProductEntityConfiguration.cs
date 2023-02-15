using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{

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

         builder.HasOne(item => item.alcoholType)
         .WithMany(item => item.Products);

         builder.HasOne(item => item.appellation)
         .WithMany(item => item.Products);

        // builder.HasMany(p => p.InvoicesFurnihserProduct)
        //     .WithOne(fp => fp.Product)
        //     .HasForeignKey(fp => fp.ProductId);

        builder.HasData(
            new Product { ProductId = 1, Name = "product 1", ImageUrl="", Price = 12, Stock = 155, Millesime = 2010, AlcoholDegree = 2, Reference = "jndijfndjn", FurnisherId = 1, DomainId = 1, RegionId = 1, AlcoholTypeId = 1, AppellationId = 1 }
        );


    }

}
