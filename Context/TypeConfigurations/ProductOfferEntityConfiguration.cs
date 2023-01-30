using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class ProductOfferEntityConfiguration : IEntityTypeConfiguration<ProductOffer>
{

    public void Configure(EntityTypeBuilder<ProductOffer> builder)
    {
        builder.ToTable("ProductOffer");
    }


}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video