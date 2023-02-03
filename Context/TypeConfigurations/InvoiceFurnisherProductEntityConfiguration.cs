using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class InvoiceFurnisherProductEntityConfiguration : IEntityTypeConfiguration<InvoiceFurnisherProduct>
{

    public void Configure(EntityTypeBuilder<InvoiceFurnisherProduct> builder)
    {
        builder.ToTable("InvoiceFurnisherProduct");
        // builder.HasKey(item => item.Id);

    }


}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video