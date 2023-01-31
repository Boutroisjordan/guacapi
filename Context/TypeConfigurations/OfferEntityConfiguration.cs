using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class OfferEntityConfiguration : IEntityTypeConfiguration<Offer>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        //Nom de la tabmle
        builder.ToTable("Offer");
        //Primary key de la table
        builder.HasKey(item => item.OfferId);

    }
    #endregion

}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video