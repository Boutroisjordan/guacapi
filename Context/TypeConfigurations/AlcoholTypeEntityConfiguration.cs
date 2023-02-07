using Microsoft.EntityFrameworkCore;
using GuacAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuacAPI.Context.TypeConfigurations;

class AlcoholTypeEntityConfiguration : IEntityTypeConfiguration<AlcoholType>
{
    #region Public methods
    public void Configure(EntityTypeBuilder<AlcoholType> builder)
    {
        //Nom de la tabmle
        builder.ToTable("alcohol_type");
        //Primary key de la table
        builder.HasKey(item => item.AlcoholTypeId);

        builder.HasData(
            new AlcoholType { AlcoholTypeId = 1, label = "Rouge"}
        );

    }
    #endregion

}

//https://medium.com/net-core/build-a-restful-web-api-with-asp-net-core-6-30747197e229 le blog
// https://www.youtube.com/watch?v=fAsZP70uiic video